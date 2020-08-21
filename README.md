1. install Grpc and Google.Protobuf

2.
** usage example: **

using System;
using Edgify;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var sdk = new Edgify.PredictionSdk("127.0.0.1", 8586);
            sdk.Connect();
            var prediction = sdk.GetPrediction();
            sdk.CreateGroundTruth(prediction, "banana");
        }
    }
}