using System;
using System.Threading.Tasks;
using grpc = Grpc.Core;

namespace Edgify
{
    public partial class EdgifySdk
    {

        public async Task<Prediction> GetPredictionAsync(string source = null)
        {
            var request = new PredictionRequest();
            if (!String.IsNullOrEmpty(source))
            {
                request.Source = source;
            }
            var response = await client.GetPredictionAsync(request);
            return response.Prediction;
        }

        public async Task<bool> CreateGroundTruthAsync(Prediction prediction, string label, string source)
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
            await this.client.CreateGroundTruthAsync(request);

            return true;
        }
        
        public async Task<bool> DeleteSampleAsync(string uuid)
        {
            if (!String.IsNullOrEmpty(uuid))
            {
                var request = new DeleteSampleRequest
                {
                    Uuid = uuid
                };
                await this.samples_client.DeleteSampleAsync(request);
            }

            return true;
        }

        public async void StartCustomerTransactionAsync()
        {
            var request = new CreateAnalyticsEventRequest
            {
                Name = "TransactionCustomerStart"
            };
            await this.analytics_client.CreateEventAsync(request);
        }

        public async void EndCustomerTransactionAsync()
        {
            var request = new CreateAnalyticsEventRequest
            {
                Name = "TransactionCustomerEnd"
            };
            await this.analytics_client.CreateEventAsync(request);
        }

        public void Disconnect()
        {
            this.channel.ShutdownAsync().Wait();
        }
    }
}
