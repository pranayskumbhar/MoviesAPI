using Azure.Core;
using Microsoft.Extensions.Options;
using Polly;
using System.Text.Json;

namespace MoviesAPI.PollyHelper
{
    public static class PollyRetryHelper
    {
        private static int _retryCount = 3; // fallback
        public static void Initialize(string configPath = "appsettings.json")
        {
            if (!File.Exists(configPath)) return;
            try
            {
                var json = File.ReadAllText(configPath);
                using var doc = JsonDocument.Parse(json);
                _retryCount = doc.RootElement
                    .GetProperty("PollySettings")
                    .GetProperty("RetryCount")
                    .GetInt32();
            }
            catch
            {
                _retryCount = 3; // fallback
            }
        }

        public static async Task<HttpResponseMessage> ExecuteApiCallWithRetryAsync(
    HttpClient client,
    HttpMethod method,
    string url,
    HttpContent? content = null)
        {
            var retryPolicy = Policy
                .Handle<HttpRequestException>()
                .OrResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                .WaitAndRetryAsync(_retryCount, attempt => TimeSpan.FromSeconds(2));

            return await retryPolicy.ExecuteAsync(() =>
            {
                var request = new HttpRequestMessage(method, url);
                if (content != null)
                    request.Content = content;

                return client.SendAsync(request);
            });
        }
    }
}
