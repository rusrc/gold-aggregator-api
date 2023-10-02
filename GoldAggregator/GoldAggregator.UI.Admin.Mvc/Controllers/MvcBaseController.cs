using GoldAggregator.Parcer.Entities.Extensions;
using GoldAggregator.Parser.Entities.Entities;
using GoldAggregator.Parser.Entities.Enums;

using Microsoft.AspNetCore.Mvc;

using System.Globalization;

using GoldAggregator.UI.Admin.Mvc.AutoMapper;

using static GoldAggregator.UI.Admin.Mvc.Constants;
using static GoldAggregator.Parser.Services.Transliteration;

namespace GoldAggregator.UI.Admin.Mvc.Controllers
{
    public class MvcBaseController : Controller
    {
        protected string GetCoinCatalogName(CoinCatalog cc)
        {
            return $"{cc?.MetalType.GetMetalName()}, {cc?.Weight * oz:##.##} Oz, {cc?.Weight} г, {cc?.Name}";
        }

        protected string GetTranslitTitle(string name, decimal weight, MetalTypeEnum metalType)
        {
            var separator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            var prefix = $"{metalType.GetMetalNounName()} монета";
            var sWeight = "вес " + weight.ToString().Replace(separator, "_") + " г";

            var title = Generate($"{prefix}_{name}_{sWeight}");

            return title;
        }

        /// <summary>
        /// Gererate image file name
        /// Example: platinovaya-moneta_klenovyj-list_31_10.png
        /// </summary>
        /// <param name="name"></param>
        /// <param name="weight"></param>
        /// <param name="metalType"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        protected string GetFileExtension(IFormFile? file)
        {
            return file != null ? Path.GetExtension(file.FileName) : ""; ;
        }
    }
}
