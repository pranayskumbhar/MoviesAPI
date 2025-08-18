using System.Net.Http;
using System.Text;

namespace MoviesAPI.ApiBehaviour
{
    public static class ApiCaller
    {
        public static async Task<HttpResponseMessage> CallApiAsync(
    string url,
    string content = null,
    HttpMethod method = null,
    string mediaType = "application/json") // you can pass "application/xml", "text/plain", etc.
        {
            method ??= HttpMethod.Get;
            var request = new HttpRequestMessage(method, url);

            if (!string.IsNullOrEmpty(content) && (method == HttpMethod.Post || method == HttpMethod.Put))
            {
                request.Content = new StringContent(content, Encoding.UTF8, mediaType);
            }

            return await new HttpClient().SendAsync(request);
        }

    }
}
