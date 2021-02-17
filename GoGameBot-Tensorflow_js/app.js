let express = require('express');
const tf = require("@tensorflow/tfjs");
const ML = require('./ml/ml.js');
let ml = new ML();

ml.initML();
ml.compileModel();
ml.fit();

app = express();

app.listen(8080);

app.get('/', (request, response) => {
    let arrayParams = [];
    for (let indexX = 0; indexX < 19; indexX++) {
        for (let indexY = 0; indexY < 19; indexY++) {
            arrayParams.push(parseInt(request.query[indexX + "." + indexY]));
        }
    }


    response.setHeader('Content-Type', 'application/json');
    response.end(ml.predict(arrayParams));
});

console.log("Code me daddy!");

// let temp = "";

// for (let indexX = 0; indexX < 19; indexX++) {
//     for (let indexY = 0; indexY < 19; indexY++) {
//         temp += "\"X"+indexX+"Y"+indexY+"\",";
//     }
// }
// for (let indexX = 0; indexX < 19; indexX++) {
//     for (let indexY = 0; indexY < 19; indexY++) {
//         temp += "\"RX"+indexX+"Y"+indexY+"\",";
//     }
// }

// console.log(temp);