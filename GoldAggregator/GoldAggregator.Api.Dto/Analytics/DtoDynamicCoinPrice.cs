namespace GoldAggregator.Api.Dto.Analytics
{
    public class DtoDynamicCoinPrice
    {
        public string CoinCatalogName { get; set; }
        public string DealerName { get; set; }
        public string ParseDate { get; set; }

        /// <summary>
        /// Цена продажи дилера. 
        /// Для пользователя это цена покупки
        /// </summary>
        public decimal? MinPriceToBuy { get; set; }
    }
}
