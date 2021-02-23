using System;
using Edgify;
using Console;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            // connection phase
            var sdk = new Edgify.PredictionSdk("127.0.0.1", 8586);
            sdk.Connect();
            
            // take a prediction
            var prediction = sdk.GetPrediction();
            
            if (prediction.certain == true) {
                Console.WriteLine("using Autobuy")
            }

            // after the transaction 
            string label = "banana";
            string source = "Autobuy";

            sdk.CreateGroundTruth(prediction, label, source);
        }
    }
}