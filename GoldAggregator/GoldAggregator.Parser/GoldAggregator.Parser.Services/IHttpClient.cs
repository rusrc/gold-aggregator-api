using System.Threading.Tasks;

namespace GoldAggregator.Parser.Services
{
    public interface IHttpClient
    {
        Task<string> GetAsync(string url);
    }
}
