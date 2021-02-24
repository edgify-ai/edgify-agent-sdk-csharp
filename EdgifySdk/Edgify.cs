using System;
using grpc = Grpc.Core;

namespace Edgify
{
    public partial class EdgifySdk
    {
        EdgifyService.EdgifyServiceClient client;
        AnalyticsService.AnalyticsServiceClient analytics_client;
        SamplesService.SamplesServiceClient samples_client;
        readonly string host;
        readonly int port;

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
            var groundTruth = new GroundTruth
            {
                Prediction = prediction,
                Label = label,
                Source = source
            };

            var request = new GroundTruthRequest
            {
                GroundTruth = groundTruth
            };
            this.client.CreateGroundTruth(request);
        }
        
        public void DeleteSample(string uuid)
        {
            if (!String.IsNullOrEmpty(uuid))
            {
                var request = new DeleteSampleRequest
                {
                    Uuid = uuid
                };
                this.samples_client.DeleteSample(request);
            }
        }

        public void StartCustomerTransaction()
        {
            var request = new CreateAnalyticsEventRequest
            {
                Name = "TransactionCustomerStart"
            };
            this.analytics_client.CreateEvent(request);
        }

        public void EndCustomerTransaction()
        {
            var request = new CreateAnalyticsEventRequest
            {
                Name = "TransactionCustomerEnd"
            };
            this.analytics_client.CreateEvent(request);
        }
    }
}
