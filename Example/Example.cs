using System;
using Edgify;

namespace Example
{
    class SyncExample
    {
        static void Main()
        {
            // connection phase
            var sdk = new EdgifySdk("127.0.0.1", 50051);
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

            // when you done with the sdk (usually on dispose) 
            sdk.Disconnect();
        }
    }

    class ASyncExample
    {
        static async void MainAsync()
        {
            // connection phase
            var sdk = new EdgifySdk("127.0.0.1", 50051);
            sdk.Connect();

            // take a prediction
            var prediction = await sdk.GetPredictionAsync();

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

            await sdk.CreateGroundTruthAsync(prediction, label, source);

            // if you need to delete a sample
            await sdk.DeleteSampleAsync(prediction.Uuid);

            // inform edgify on transaction start
            sdk.StartCustomerTransactionAsync();

            // inform edgify on transaction end
            sdk.EndCustomerTransactionAsync();

            // when you done with the sdk (usually on dispose) 
            sdk.Disconnect();
        }
    }
}
