namespace GoldAggregator.Api.Dto
{
    public class CoinCatalogDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// CoinCatalog name in translit
        /// </summary>
        public string TranslitName { get; set; }
        /// <summary>
        /// CoinCatalog image
        /// </summary>
        public string ImageName { get; set; }
        public int MintCountryId { get; set; }
        public string MintCountryName { get; set; }
        public string MintCountryTranslitName { get; set; }
        public string SeoUrl { get; set; }
        public decimal Weight { get; set; }
        public int MetalType { get; set; }
        public string MetalTypeName { get; set; }
        /// <summary>
        /// Покупка От.
        /// </summary>
        public decimal? MinPriceToSell { get; set; }     
        public decimal? MaxPriceToSell { get; set; }
        public decimal? MinPriceToBuy { get; set; }
        /// <summary>
        /// это Выкуп до, или Покупка до
        /// </summary>
        public decimal? MaxPriceToBuy { get; set; }
        /// <summary>
        /// // Покупка От.
        /// </summary>
        public decimal? MinPriceToSellPerGram { get; set; }
        // public decimal? MaxPriceToSellPerGram { get; set; }
        public decimal? MinPriceToBuyPerGram { get; set; }

        /// <summary>
        /// это Выкуп до, или Покупка до
        /// </summary>
        public decimal? MaxPriceToBuyPerGram { get; set; }
    }
}
