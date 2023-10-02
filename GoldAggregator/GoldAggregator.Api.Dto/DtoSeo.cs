using System.Text.Json.Serialization;

namespace GoldAggregator.Api.Dto
{
    public class DtoSeo
    {
        /// <summary>
        /// For seo meta=title 
        /// </summary>
        [JsonPropertyName("seoTitle")]
        public string seoTitle { get; set; } = "Gold Katalog";

        /// <summary>
        /// For seo meta=description 
        /// </summary>
        [JsonPropertyName("seoDescription")]
        public string seoDescription { get; set; } = "Gold Katalog";

        /// <summary>
        /// For h1 in html
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; } = "Gold Katalog";

        /// <summary>
        /// Description for html body
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; } = "Gold Katalog";

        /// <summary>
        /// https://github.com/RehanSaeed/Schema.NET
        /// </summary>
        [JsonPropertyName("jsonLd")]
        public object JsonLd { get; set; }
    }
}
