using System;

namespace GoldAggregator.Parser.Dto
{
    public  class CoinDto
    {
        /// <summary>
        /// Наименование позиции у дилера
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Адрес карточки монеты на сайте
        /// </summary>
        public string Url { get; set; }      

        /// <summary>
        /// Номинал
        /// </summary>
        public string Nomination { get; set; }
        /// <summary>
        /// Вес
        /// </summary>
        public decimal Weight { get; set; }
        /// <summary>
        /// Основная цена 
        /// </summary>
        public decimal? PriceToBuy { get; set; }
        /// <summary>
        /// Цена со скидкой
        /// </summary>
        public decimal? PriceToSell { get; set; }
        /// <summary>
        /// Допольнительная цена со всякой особой скидкой, клупной скидкой и другой ерундой
        /// </summary>
        public decimal? PriceSpecial { get; set; }
        /// <summary>
        /// Дата парсинга
        /// </summary>
        public DateTime ParseDate { get; set; }
        /// <summary>
        /// Золото / Серебро / Платина / Палладий
        /// </summary>
        public int MetalType { get; set; }
        public DateTime? StartMiningYear { get; set; }
        public DateTime? EndMiningYear { get; set; }
        /// <summary>
        /// Описание ошибки при парсинги карточки монеты
        /// </summary>
        public string Error { get; set; }

    }
}
