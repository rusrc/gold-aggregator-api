
using System.Threading;
using System.Threading.Tasks;

namespace GoldAggregator.Parser.Managers
{
    public interface ISaveItemsManager
    {
        Task RunAsync(CancellationToken cancellationToken);
    }
}
