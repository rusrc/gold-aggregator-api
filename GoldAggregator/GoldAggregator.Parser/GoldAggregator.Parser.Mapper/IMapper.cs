using System.Threading.Tasks;

namespace GoldAggregator.Parser.Mapper
{
    public interface IMapper<TTarget, in TSource>
    {
        Task<TTarget> MapAsync(TSource source);
    }
}
