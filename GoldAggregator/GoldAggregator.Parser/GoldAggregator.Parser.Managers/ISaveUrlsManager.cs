
using System.Threading;
using System.Threading.Tasks;

namespace GoldAggregator.Parser.Managers
{
    public interface ISaveUrlsManager
    {
        Task RunAsync(CancellationToken cancellationToken);
    }
}
