namespace GoldAggregator.Infrastructure.Repositories
{
    public interface IDealerRepository
    {
        Task<int[]?> GetDealerIdsAsync(int cityId);
    }
}
