using System;
using Edgify;
using grpc = global::Grpc.Core;

namespace Edgify
{
    public class PredictionSdk
    {
        EdgifyService.EdgifyServiceClient client;
        string host;
        int port;

        public PredictionSdk(string host, int port)
        {
            this.host = host;
            this.port = port;
        }

        public void Connect()
        {
            grpc.Channel channel = new grpc.Channel(host, port, grpc.ChannelCredentials.Insecure);
            this.client = new EdgifyService.EdgifyServiceClient(channel);
        }

        public Prediction GetPrediction()
        {
            var request = new PredictionRequest();
            var response = client.GetPrediction(request);
            return response.Prediction;
        }

        public void CreateGroundTruth(Prediction prediction, string label)
        {
            var groundTruth = new GroundTruth();
            groundTruth.Prediction = prediction;
            groundTruth.Label = label;

            var request = new GroundTruthRequest();
            request.GroundTruth = groundTruth;
            var response = this.client.CreateGroundTruth(request);
        }

    }
}
