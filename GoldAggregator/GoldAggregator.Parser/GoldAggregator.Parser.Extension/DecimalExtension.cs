using System.Globalization;

namespace GoldAggregator.Parser.Extension
{
    public static class DecimalExtension
    {
        public static string ToRublePrice(this decimal price)
        {
            return price.ToString("C0", CultureInfo.CreateSpecificCulture("ru-RU"));
        }

        public static string ToRublePrice(this decimal? price)
        {
            return price?.ToString("C0", CultureInfo.CreateSpecificCulture("ru-RU"));
        }
    }
}
