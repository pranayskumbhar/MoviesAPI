using MoviesAPI.StaticDetails;
using System.Text;

namespace MoviesAPI.CommonFunctionalities
{
    public class OpenAiService
    {
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;

        public OpenAiService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _apiKey = configuration["keys:OpenAISecretKey"];
        }

        public async Task<string> ExplainExceptionAsync(Exception ex)
        {
            string prompt = $"Explain and suggest a fix for this exception:\n{ex}";

            var requestBody = new
            {
                model = "gpt-4", // or "gpt-3.5-turbo"
                messages = new[]
                {
                new { role = "system", content = "You are a helpful assistant." },
                new { role = "user", content = prompt }
            }
            };

            var requestContent = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(requestBody),
                Encoding.UTF8,
                "application/json");

            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);

            var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", requestContent);
            var result = await response.Content.ReadAsStringAsync();

            var json = System.Text.Json.JsonDocument.Parse(result);
            return json.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();
        }
    }

}
