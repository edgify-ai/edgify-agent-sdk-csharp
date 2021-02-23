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
            
            // Autobuy flag
            if (prediction.certain == true) {
                Console.WriteLine("using Autobuy")
            }

            // after the transaction create the ground truth
            string label = "banana";
            string source = "Autobuy";

            sdk.CreateGroundTruth(prediction, label, source);

            // if you need to delete a sample
            sdk.DeleteSample(prediction.Uuid);

            // inform edgify on transaction start
            sdk.StartCustomerTransaction();

            // inform edgify on transaction end
            sdk.EndCustomerTransaction();
        }
    }
}