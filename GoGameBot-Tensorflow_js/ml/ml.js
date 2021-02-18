
const tf = require("@tensorflow/tfjs");
const fs = require('fs');
const csvParser = require('csv-parser');

const FIRSTLAYERCOUNT = 19*19;
const INTERVALBATCH = 50;
const ADDERINCREMENT = 18;
let batchNumber = 0;

class ML {
    constructor() {
        this.model = null;
        this.intervalBatch = 10;
        this.indexBatch = 0;
        this.bannedValue = [];
        this.generationCount = 1;
    }
    initML() {
        console.log("|- Init ml point");
        this.model = tf.sequential({
            layers: [
                tf.layers.dense({inputShape: [FIRSTLAYERCOUNT], units: FIRSTLAYERCOUNT, activation: 'softmax'}),
                tf.layers.dense({units: FIRSTLAYERCOUNT, activation: 'softmax'}),
                tf.layers.dense({units: (19 * 10), activation: 'relu'}),
                tf.layers.dense({units: 19, activation: 'relu'}),
                tf.layers.dense({units: 19, activation: 'relu'}),
                tf.layers.dense({units: (19 * 10), activation: 'relu'}),
                tf.layers.dense({units: FIRSTLAYERCOUNT, activation: 'softmax'}),
            ]
        });

        // this.model.weights.forEach(w => {
        //     console.log(w.name, w.shape);
        // });
           
    }
    compileModel() {
        console.log("|- Compile ml point");
        this.model.compile({
            optimizer: 'sgd',
            loss: 'categoricalCrossentropy',
            metrics: ['accuracy']
        });
    }

    fit() {
        console.log(`|= Generations Count: ${this.generationCount}`);
        this.generationCount += 1;
        loadCSV().then((result) => {
            // fit when datas is successfully loaded
            let shape = [result.inputs.length, FIRSTLAYERCOUNT];
            // console.log(tf.tensor2d(result.inputs, shape).shape);
            this.model.fit(tf.tensor2d(result.inputs, shape), tf.tensor2d(result.outputs, shape), {
                epochs: 10,
                batchSize: 64,
                callbacks: {onBatchEnd}
            }).then(info => {
                console.log('|- Final accuracy: ', info.history.acc);
                console.log('|- Batchs numbers: ', batchNumber);
            });
        });
    }

    predict(arrayParams) {
        let adder = 0;
        let input2D = [];
        for (let indexX = 0; indexX < 19; indexX++) {
            input2D[indexX] = [];
            for (let indexY = 0; indexY < 19; indexY++) {
                input2D[indexX][indexY] = arrayParams[indexX + indexY + adder];
            }
            adder += ADDERINCREMENT;
        }
        adder = 0; // reset of the adder

        //let shape = [FIRSTLAYERCOUNT, 1];
        let input = tf.tensor2d([arrayParams]); //, shape);

        let max = 0;
        let valueX = -1, valueY = -1;

        do {
            let predicted = this.model.predict(input).dataSync();

            // for (let index = 0; index < 19 * 19; index++) {
            //     // console.log(index + ":" + predicted[index]);
            //     console.log(`index: ${index} value: ${arrayParams[index]}`);
            // }

            let predicted2d = [];
            for (let indexX = 0; indexX < 19; indexX++) {
                predicted2d[indexX] = [];
                for (let indexY = 0; indexY < 19; indexY++) {
                    predicted2d[indexX][indexY] = predicted[indexX + indexY + adder];
                }
                adder += ADDERINCREMENT;
            }
            adder = 0; // reset of the adder


            // console.log(predicted);
            for (let indexX = 0; indexX < 19; indexX++) {
                for (let indexY = 0; indexY < 19; indexY++) {
                    if (contains(input2D, {x: indexX, y: indexY})) {
                        continue;
                    }
                    // console.log(max);
                    if (predicted2d[indexX][indexY] > max) {
                        max = predicted2d[indexX][indexY];
                        valueX = indexX;
                        valueY = indexY;
                    }
                }
            }
        } while(input2D[valueX][valueY] != 0);
        max = 0; // reset of max value

        console.log(`Case predicted: x: ${valueX} y: ${valueY}`);

        return JSON.stringify({ x: valueX, y: valueY });
    }
}

function contains(array, input) {
    for (let indexX = 0; indexX < 19; indexX++) {
        for (let indexY = 0; indexY < 19; indexY++) {
            if (array[input.x][input.y] != 0) {
                return true;
            }
        }
    }
    return false;
}

function loadCSV() {
    let tensorInputs = [];
    let tensorOutputs = [];
    return new Promise((resolve, reject) => {
        fs.createReadStream("C:\\Temp\\data_ml.csv")
            .pipe(csvParser())
            .on('data', (row) => {
                let tempTensorInput = [];
                let tempTensorOutput = [];
                for (let indexX = 0; indexX < 19; indexX++) {
                    // tempTensorInput[indexX] = [];
                    // tempTensorOutput[indexX] = [];
                    for (let indexY = 0; indexY < 19; indexY++) {
                        tempTensorInput/*[indexX]*/.push(parseInt(row["X"+indexX+"Y"+indexY]));
                        tempTensorOutput/*[indexX]*/.push(parseInt(row["RX"+indexX+"Y"+indexY]));
                    }
                }
                tensorInputs.push(tempTensorInput);
                tensorOutputs.push(tempTensorOutput);
            })
            .on('end', () => {
                console.log("|- CSV file loaded successfully");

                return resolve({inputs: tensorInputs, outputs: tensorOutputs});
            });
    });
}
function onBatchEnd(batch, logs) {
    if (batch % INTERVALBATCH == 0) {
        console.log('|- Accuracy', logs.acc);
    }
    batchNumber += 1;
}


module.exports = ML;