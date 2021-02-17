
const tf = require("@tensorflow/tfjs");
const fs = require('fs');
const csvParser = require('csv-parser');

const FIRSTLAYERCOUNT = 19*19;

class ML {
    constructor() {
        this.model = null;
    }
    initML() {
        this.model = tf.sequential({
            layers: [
                tf.layers.dense({inputShape: [FIRSTLAYERCOUNT], units: FIRSTLAYERCOUNT, activation: 'softmax'}),
                tf.layers.dense({units: 19, activation: 'softmax'}),
                tf.layers.dense({units: FIRSTLAYERCOUNT, activation: 'softmax'}),
            ]
        });

        // this.model.weights.forEach(w => {
        //     console.log(w.name, w.shape);
        // });
           
    }
    compileModel() {
        console.log("compile ml point");
        this.model.compile({
            optimizer: 'sgd',
            loss: 'categoricalCrossentropy',
            metrics: ['accuracy']
        });
    }

    fit() {
        loadCSV().then((result) => {
            // fit when datas is successfully loaded
            let shape = [result.inputs.length, 361];
            console.log(tf.tensor2d(result.inputs, shape).shape)
            this.model.fit(tf.tensor2d(result.inputs, shape), tf.tensor2d(result.outputs, shape), {
                epochs: 5,
                batchSize: 32,
                callbacks: {onBatchEnd}
            }).then(info => {
                console.log('Final accuracy', info.history.acc);
            });
        });
    }

    predict(arrayParams) {
        //let shape = [FIRSTLAYERCOUNT, 1];
        let input = tf.tensor2d([arrayParams]); //, shape);

        let max = 0;
        let valueX = 0, valueY = 0;

        do {
            let predicted = this.model.predict(input).dataSync();

            for (let index = 0; index < 19 * 19; index++) {
                console.log(predicted[index]);
            }

            for (let index = 0; index < 19 * 19; index++) {
                if (predicted[index] > max) {
                    valueX = Math.round(index % 19);
                    valueY = index % 19;
                }
            }
        } while(arrayParams[valueX * valueY] != 0);

        return JSON.stringify({ x: valueX, y: valueY });
    }
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
                    for (let indexY = 0; indexY < 19; indexY++) {
                        tempTensorInput.push(parseInt(row["X"+indexX+"Y"+indexY]));
                        tempTensorOutput.push(parseInt(row["RX"+indexX+"Y"+indexY]));
                    }
                }
                tensorInputs.push(tempTensorInput);
                tensorOutputs.push(tempTensorOutput);
            })
            .on('end', () => {
                console.log("csv file loaded successfully");

                return resolve({inputs: tensorInputs, outputs: tensorOutputs});
            });
    });
}

function onBatchEnd(batch, logs) {
    console.log('Accuracy', logs.acc);
}

module.exports = ML;