using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GoldAggregator.Parser.Services
{
    /// <summary>
    /// To check socket connections type in cmd 'netstat'
    /// 
    /// Use IHttpClientFactory
    /// https://docs.microsoft.com/ru-ru/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests
    /// https://docs.microsoft.com/ru-ru/aspnet/core/fundamentals/http-requests?view=aspnetcore-6.0
    /// </summary>
    public class HttpBaseClient : IHttpClient
    {
        private readonly HttpClient _httpClient;

        public HttpBaseClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetAsync(string url)
        {
            // Register windows-1254
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var htmlPage = await _httpClient.GetStringAsync(url);
            _httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/101.0.4951.67 Safari/537.36");

            //var response = await client.GetByteArrayAsync(url);
            //var htmlPage = Encoding.UTF8.GetString(response, 0, response.Length - 1);

            return htmlPage;
        }
    }
}
