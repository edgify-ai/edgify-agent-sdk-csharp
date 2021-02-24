using System;
using Edgify;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            // connection phase
            var sdk = new Edgify.EdgifySdk("127.0.0.1", 50051);
            sdk.Connect();

            // take a prediction
            var prediction = sdk.GetPrediction();

            // Autobuy flag
            if (prediction.Certain == true)
            {
                Console.WriteLine("using Autobuy");
            }

            Console.WriteLine("Uuid: " + prediction.Uuid);
            Console.WriteLine("Predictions: " + prediction.Predictions);

            // after the transaction create the ground truth
            string label = "banana";
            string source = "RegularMenuSelection";

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
