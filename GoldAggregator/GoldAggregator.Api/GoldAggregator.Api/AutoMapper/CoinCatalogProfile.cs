using AutoMapper;

using GoldAggregator.Api.Dto;
using GoldAggregator.Parcer.Entities.Extensions;
using GoldAggregator.Parser.Entities.Entities;
using GoldAggregator.Parser.Services;

namespace GoldAggregator.Api.AutoMapper
{
    /// <summary>
    /// CoinCatalogProfile
    /// </summary>
    public class CoinCatalogProfile : Profile
    {
        private readonly RussianDeclineService _declienService;

        public CoinCatalogProfile()
        {
            _declienService = new RussianDeclineService();

            // TODO remove. Not used but check before delete
            CreateMap<CoinCatalog, CoinCatalogDto>()
                .ForMember(dest => dest.SeoUrl, opt => opt.MapFrom(m => $"{m.SeoUrl}_{m.Weight}_{m.Id}"))
                .ForMember(dest => dest.MetalTypeName, opt => opt.MapFrom(m => m.MetalType.GetMetalName()))
                .ForMember(dest => dest.MinPriceToSell, opt => opt.MapFrom(m => m.CoinPrices.Any() ? m.CoinPrices.Min(p => p.PriceToSell) : 0))
                .ForMember(dest => dest.MaxPriceToBuy, opt => opt.MapFrom(m => m.CoinPrices.Any() ? m.CoinPrices.Max(p => p.PriceToBuy) : 0))
                ;

            CreateMap<City, DtoCity>()
                .ForMember(dest => dest.NameGenitive, opt => opt.MapFrom(m => _declienService.GetGenitive(m.Name)))
                .ForMember(dest => dest.NameDative, opt => opt.MapFrom(m => _declienService.GetDative(m.Name)))
                .ForMember(dest => dest.NameAccusative, opt => opt.MapFrom(m => _declienService.GetAccusative(m.Name)))
                .ForMember(dest => dest.NameInstrumental, opt => opt.MapFrom(m => _declienService.GetInstrumental(m.Name)))
                ;
        }
    }
}
