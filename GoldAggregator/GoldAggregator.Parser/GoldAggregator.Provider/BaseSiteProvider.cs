using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Text;
using GoldAggregator.Parser.Exceptions;
using GoldAggregator.Parser.Provider.Attributes;
using GoldAggregator.Parser.Provider.Exceptions;
using GoldAggregator.Parser.Extension;

namespace GoldAggregator.Parser.Provider
{
    public class BaseSiteProvider
    {
        public string ProviderName => GetType().Name;

        protected virtual string GetRootHost()
        {
            var descriptionAttribute = (ProviderDescriptionAttribute)this.GetType()
                .GetCustomAttribute(typeof(ProviderDescriptionAttribute), true);


            var hostName = Regex.Match(descriptionAttribute?.Host ?? "", @"(https?://[\w\.\d\W]+)",
                RegexOptions.IgnoreCase | RegexOptions.Multiline).Groups[1].Value;

            if (string.IsNullOrEmpty(hostName))
            {
                throw new ProviderException(
                    ProviderName, $@"Can't get the value from DescriptionAttribute provided on siteProvider {ProviderName}. 
                       Please provide the {ProviderName} with attribute [Description(""host:https://www.example.ru"")]");
            }

            return hostName;
        }

        protected decimal ToDecimal(string numberText)
        {
            numberText = numberText.Replace(',', '.');
            if (numberText.IsEmpty()) throw new InvalidOperationException($"can't parse empty '{numberText}'");

            if (decimal.TryParse(numberText, NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
            {
                return result;
            }

            throw new InvalidOperationException($"can't parse '{numberText}'");
        }

        /// <summary>
        /// Get price from text
        /// </summary>
        /// <param name="priceText">Text contains price. For example Цена: от 38500 руб.</param>
        /// <returns></returns>
        protected virtual decimal GetPriceFromText(string priceText)
        {
            if (priceText == null) throw new ArgumentNullException($"'{nameof(priceText)}' param can't be null");

            var expression = @"(?<price>(?<=^| )?\d+([,\.\s]\d+)?(?=$| |)|(?<=^| )[,\.\s]\d+(?=$| ))";
            var number = Regex.Match(priceText.RemoveSpaces(), expression).Groups["price"].Value.Replace('.', ',');

            var price = ToDecimal(number);
            if (price > 0) return price;

            throw new PriceNotFoundException(ProviderName);
        }

        protected virtual bool GetWeightFromText(string text, out string weight)
        {
            weight = null;
            var matchedWeight = false;
            string previousClearText=String.Empty;
            StringBuilder sb = new StringBuilder(text.ToLower());
            //Вырезаем пробы
            sb.Replace(" ", "")
                .Replace(",", ".")
                .Replace("унции", "oz")
                .Replace("унций", "oz")
                .Replace("унция", "oz")
                .Replace("0.900", "")
                .Replace("900", "")
                .Replace("0.9999", "")
                .Replace("0.999", "")
                .Replace("999", "")
                .Replace("0.9995", "")
                .Replace("0.995", "")
                .Replace("995", "")
                .Replace("0.925", "")
                .Replace("925", "");

            //Вырезаем года
            for (int i = 1700; i< 2050; i++)
            {
                sb.Replace(i.ToString(), "");
            }

            var clearText = sb.ToString();
           
                        var Oz2 = @"[2][Oo][Zz]";
            var Oz1 = @"[1][Oo][Zz]";
            var Oz1_2 = @"[1]\/[2][Oo][Zz]";
            var Oz1_4 = @"[1]\/[4][Oo][Zz]";
            var Oz1_10 = @"[1]\/[10][Oo][Zz]";
            var Oz1_20 = @"[1]\/[20][Oo][Zz]";
            // "999.999(г|Г) | 999,999(г|Г) "
            var w999_999 = @"(?<weight>\d{1,4}(\.|\,)\d{1,3})( |[гГ])?";
            var w999_999Reg = new Regex(w999_999, RegexOptions.CultureInvariant);
            
            // "999(г|Г) | 999(г|Г) "
            var w999 = @"(?<weight>\d{1,4})( |[гГ])?";
            var w999Reg = new Regex(w999, RegexOptions.CultureInvariant);           

            const string expression = @"(?<weight>\d{1,4}[\.\,]?\d{1,3}\s*(?<measure>[гГgG]р?)\.?)";
            var regexp = new Regex(expression, RegexOptions.CultureInvariant);

            while(previousClearText != clearText)
            {
                previousClearText = clearText;

                var w999_999Match = w999_999Reg.Match(clearText);
                var w999Match = w999Reg.Match(clearText);
                var match = regexp.Match(clearText);

                if (Regex.IsMatch(clearText, Oz2))
                {
                    weight = "62.2";
                    matchedWeight = true;
                }
                else
                if (Regex.IsMatch(clearText, Oz1))
                {
                    weight = "31.1";
                    matchedWeight = true;
                }
                else
                if (Regex.IsMatch(clearText, Oz1_2))
                {
                    weight = "15.55";
                    matchedWeight = true;
                }
                else
                if (Regex.IsMatch(clearText, Oz1_4))
                {
                    weight = "7.78";
                    matchedWeight = true;
                }
                else
                if (Regex.IsMatch(clearText, Oz1_10))
                {
                    weight = "3.11";
                    matchedWeight = true;
                }
                else
                if (Regex.IsMatch(clearText, Oz1_20))
                {
                    weight = "1.56";
                    matchedWeight = true;
                }
                else
                if (w999_999Match.Success)
                {
                    weight = w999_999Match.Groups["weight"].Value;
                    matchedWeight = true;
                }
                else
                if (w999Match.Success)
                {
                    weight = w999Match.Groups["weight"].Value;
                    matchedWeight = true;
                }
                else
                if (match.Success)
                {
                    weight = match.Groups["weight"].Value;
                    matchedWeight = true;
                }

                if (matchedWeight)
                {
                    var weightNumber = ToDecimal(weight);
                    //Если это проба т.е. <1  или это год т.к. число от 1700 о 2050 это год
                    
                    if (weightNumber < 1 || (weightNumber >= 1700 && weightNumber <= 2050))
                    {   //зачищаем некорректный вес и пробуем еще раз
                        clearText = clearText.Replace(weight, "");
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return false;
            
        }

        protected string GetStartingYearFromText(string text)
        {
            /*   Шпаргалка
             * [a-z] является обязательным
             * ([a-z])? необязательно, с группой захвата
             * (?:[a-z])? без захвата группы, просто необязательно
             */

            var options = RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant;
            var year = "year";
            var patterns = new List<string>
            {

                $@"(?<{year}>[12][0-9]{{3}})(\s*\,\s*[12][0-9]{{3}})+\s*(?<yearName>[гГ]\.?(?:од)?(?:[вВ]\.?)?)",       // 1976,1977,1979,1980,1981  гг.в
                $@"(?<{year}>[12][0-9]{{3}})\s*?\-\s*?(?:[12][0-9]{{3}})\s*?(?<yearName>[гГ]\.?(?:од)?(?:[вВ]\.?)?)",   // 1000 - 2999 (г.|Г.|г.в.|год|Год)
                @$"(?<{year}>[12][0-9]{{3}})\s*?(?<yearName>[гГ]\.?(?:од)?(?:[вВ]\.?)?)",                               // 2999 (г.|Г.|г.в.|год|Год)
                $@"(?<{year}>[12][0-9]{{3}})",
            };

            foreach (var pattern in patterns)
            {
                var match = Regex.Match(text, pattern, options);
                if (match.Success)
                {
                    return match.Groups[year].Value;
                }
            }

            return null;
        }

        protected string GetEndingYearFromText(string text)
        {
            /*   Шпаргалка
             * [a-z] является обязательным
             * ([a-z])? необязательно, с группой захвата
             * (?:[a-z])? без захвата группы, просто необязательно
             */

            var options = RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant;
            var year = "year";
            var patterns = new List<string>
            {
                $@"(?:[12][0-9]{{3}})(\s*\,\s*(?<{year}>[12][0-9]{{3}}))+\s*(?<yearName>[гГ]\.?(?:од)?(?:[вВ]\.?)?)",  // 1976,1977,1979,1980,1981  гг.в
                $@"(?:[12][0-9]{{3}})\s*?\-\s*?(?<{year}>[12][0-9]{{3}})\s*?(?<yearName>[гГ]\.?(?:од)?(?:[вВ]\.?)?)",  // 1000 - 2999 (г.|Г.|г.в.|год|Год)
                @$"(?<{year}>[12][0-9]{{3}})\s*?(?<yearName>[гГ]\.?(?:од)?(?:[вВ]\.?)?)",                              // 2999 (г.|Г.|г.в.|год|Год)
                $@"(?<{year}>[12][0-9]{{3}})",
            };

            foreach (var pattern in patterns)
            {
                var match = Regex.Match(text, pattern, options);
                if (match.Success)
                {
                    return match.Groups[year].Value;
                }
            }

            return null;
        }

        protected int? GetMetalType(string text)
        {
            /// Золото
            /// Серебро
            /// Платина
            /// Палладий

            var gold = @"[зЗ]олот[оаы]?[йяе]?";
            var silver = @"[сС]еребр(о|а|енные|енная|яная)";
            var platinum = @"[пП](латин|латен)(а|ы|ные|ная|ный)";
            var palladium = @"[пП](алади)(й|я)";

            if (Regex.IsMatch(text, gold)) return 1;
            if (Regex.IsMatch(text, silver)) return 2;
            if (Regex.IsMatch(text, platinum)) return 3;
            if (Regex.IsMatch(text, palladium)) return 4;

            return null;
        }

        /// <summary>
        /// For testing purposes
        /// </summary>
        protected async Task<string> GetFileTextAsync(string fileName)
        {
            var path = System.IO.Path.Combine(Environment.CurrentDirectory, "SitePages", fileName);
            var content = await System.IO.File.ReadAllTextAsync(path, System.Text.Encoding.UTF8);

            return content;
        }
    }
}
