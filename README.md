1. install Grpc and Google.Protobuf

2.
** usage example: **

using System;
using Edgify;
using grpc = global::Grpc.Core;

namespace SdkUsageExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var example = new EdgifyServiceClientExample();
            example.CreateNewClient();
            var prediction = example.CallGetPrediction();
            example.CallCreateGroundTruth(prediction, "banana");
        }
    }

    class EdgifyServiceClientExample
    {
        EdgifyService.EdgifyServiceClient client;

        public void CreateNewClient()
        {
            Console.WriteLine("creating new client on 172.18.0.3:50051");

            grpc.Channel channel = new grpc.Channel("172.18.0.3", 50051, grpc.ChannelCredentials.Insecure);
            this.client = new EdgifyService.EdgifyServiceClient(channel);
        }

        public Prediction CallGetPrediction()
        {
            Console.WriteLine("calling GetPrediction");

            var request = new PredictionRequest();
            var response = client.GetPrediction(request);
            return response.Prediction;
        }

        public void CallCreateGroundTruth(Prediction prediction, string label)
        {
            Console.WriteLine("calling CreateGroundTruth");

            var groundTruth = new GroundTruth();
            groundTruth.Prediction = prediction;
            groundTruth.Label = label;

            var request = new GroundTruthRequest();
            request.GroundTruth = groundTruth;
            var response = this.client.CreateGroundTruth(request);
        }

    }
}

