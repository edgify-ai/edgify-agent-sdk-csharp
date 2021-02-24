using System;
using grpc = Grpc.Core;

namespace Edgify
{
    public class EdgifySdk
    {
        EdgifyService.EdgifyServiceClient client;
        AnalyticsService.AnalyticsServiceClient analytics_client;
        SamplesService.SamplesServiceClient samples_client;

        string host;
        int port;

        public EdgifySdk(string host, int port)
        {
            this.host = host;
            this.port = port;
        }

        public void Connect()
        {
            grpc.Channel channel = new grpc.Channel(host, port, grpc.ChannelCredentials.Insecure);
            this.client = new EdgifyService.EdgifyServiceClient(channel);
            this.analytics_client = new AnalyticsService.AnalyticsServiceClient(channel);
            this.samples_client = new SamplesService.SamplesServiceClient(channel);
        }

        public Prediction GetPrediction(string source = null)
        {
            var request = new PredictionRequest();
            if (!String.IsNullOrEmpty(source))
            {
                request.Source = source;
            }
            var response = client.GetPrediction(request);
            return response.Prediction;
        }

        public void CreateGroundTruth(Prediction prediction, string label, string source)
        {
            var groundTruth = new GroundTruth();
            groundTruth.Prediction = prediction;
            groundTruth.Label = label;
            groundTruth.Source = source;

            var request = new GroundTruthRequest();
            request.GroundTruth = groundTruth;
            var response = this.client.CreateGroundTruth(request);
        }
        
        public void DeleteSample(string uuid)
        {
            if (!String.IsNullOrEmpty(uuid))
            {
                var request = new DeleteSampleRequest();
                request.Uuid = uuid;
                this.samples_client.DeleteSample(request);
            }
        }

        public void StartCustomerTransaction()
        {
            var request = new CreateAnalyticsEventRequest();
            request.Name = "TransactionCustomerStart";
            this.analytics_client.CreateEvent(request);
        }

        public void EndCustomerTransaction()
        {
            var request = new CreateAnalyticsEventRequest();
            request.Name = "TransactionCustomerEnd";
            this.analytics_client.CreateEvent(request);
        }
    }
}
