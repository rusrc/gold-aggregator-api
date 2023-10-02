using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace GoldAggregator.Parser.Extension
{
    public static class StringExtension
    {
        /// <summary>
        /// Remove spaces from string ' 233 044 4 ' -> '2330444'
        /// </summary>
        public static string RemoveSpaces(this string that)
        {
            return string.Concat(that.Where(c => !char.IsWhiteSpace(c)));
        }

        /// <summary>
        /// Short form of string.IsNullOrEmpty(that)
        /// </summary>
        public static bool IsEmpty(this string that)
        {
            return string.IsNullOrEmpty(that);
        }

        public static bool IsNotEmpty(this string that)
        {
            return !string.IsNullOrEmpty(that);
        }

        public static string ToJson(this IEnumerable<object> items, bool usePretty = false)
        {
            return JsonSerializer.Serialize(items, new JsonSerializerOptions
            {
                // Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = usePretty
            });
        }

        public static string ToJson(this object item, bool usePretty = false)
        {
            return JsonSerializer.Serialize(item, new JsonSerializerOptions
            {
                // Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = usePretty
            });
        }

        public static TObject FromJson<TObject>(this string item)
        {
            return JsonSerializer.Deserialize<TObject>(item);
        }

        public static string ToLowerFirstLetter(this string text)
        {
            //                             text.Substring(1)
            return char.ToLower(text[0]) + text[1..];
        }
    }
}
