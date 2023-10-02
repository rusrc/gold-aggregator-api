using AutoMapper;

using GoldAggregator.Parser.Entities.Entities;
using GoldAggregator.UI.Admin.Mvc.Models;



namespace GoldAggregator.UI.Admin.Mvc.AutoMapper
{
    /// <summary>
    /// CoinCatalogProfile
    /// </summary>
    public class CoinCatalogProfile : Profile
    {
        public CoinCatalogProfile()
        {
            CreateMap<CoinCatalog, CoinCatalogViewModel>();
        }
    }
}
