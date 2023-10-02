namespace GoldAggregator.Parcer.Entities.Entities
{
    public class SeoEntity
    {
        /// <summary>
        /// User-friendly URL.
        /// E.g. buy-something-cool-from-dealer-huawei
        /// </summary>
        public string SeoUrl { get; set; }
        /// <summary>
        ///  Meta tag desription
        /// <meta name='description' content='...' />
        /// </summary>
        public string SeoDescription { get; set; }
        /// <summary>
        ///  Meta tag keywords
        /// <meta name='keywords' content='...' />
        /// </summary>
        public string SeoKeyWords { get; set; }
        /// <summary>
        ///  Html title
        /// <title>...</title>
        /// </summary>
        public string SeoTitle { get; set; } 
        
    }
}
