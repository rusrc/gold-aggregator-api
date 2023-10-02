using GoldAggregator.Parser.Entities.Enums;

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

using System.ComponentModel.DataAnnotations;

namespace GoldAggregator.UI.Admin.Mvc.Models
{
    // [ModelBinder(BinderType = typeof(DecimalModelBinder))]
    public class CoinCatalogViewModel
    {
        public int Id { get; set; }
        public string? SeoUrl { get; set; }
        [Required] public string? SeoTitle { get; set; }
        [Required] public string? SeoDescription { get; set; }
        [Required] public string? SeoKeyWords { get; set; }
        [Required] public string Name { get; set; }
        /// <summary>
        /// Транцлитирация на русском
        /// </summary>
        [Display(Name = "Транцлитирация")] public string? TranslitName { get; set; }
        /// <summary>
        /// Номинал
        /// </summary>
        [Display(Name = "Номинал")] public string? Nomination { get; set; }
        /// <summary>
        /// Вес 
        /// </summary>
        // TODO "ValidateNever" could be removed
        [Required, ValidateNever] public decimal Weight { get; set; }
        /// <summary>
        /// Id страны монетного двора
        /// </summary>
        public int MintCountryId { get; set; }
        /// <summary>
        /// Имя страны монетного двора
        /// </summary>
        [Display(Name = "Монетной двор")] public string? MintCountryName { get; set; }
        /// <summary>
        /// Золото / Серебро / Платина / Палладий
        /// </summary>
        [Display(Name = "Метал")] public MetalTypeEnum MetalType { get; set; }
        /// <summary>
        /// Начальная дата выпуска. Если нет месяца и дня, пишем '01.01.1975'
        /// </summary>
        [Display(Name = "Start year")] public DateTime? StartMiningYear { get; set; }
        /// <summary>
        /// Последняя дата выпуска. Если нет месяца или дня, пишем '31.12.2022'
        /// </summary>
        [Display(Name = "End year")] public DateTime? EndMiningYear { get; set; }
        /// <summary>
        /// Уникальное имя фото
        /// </summary>
        [Display(Name = "Уникальное фото")] public string? ImageName { get; set; }
        /// <summary>
        /// Аверс
        /// </summary>
        [Display(Name = "Аверс монеты")] public IFormFile? FileObverse { get; set; }
        /// <summary>
        /// Реверс
        /// </summary>
        [Display(Name = "Реверс монеты")] public IFormFile? FileReverse { get; set; }
    }
}
