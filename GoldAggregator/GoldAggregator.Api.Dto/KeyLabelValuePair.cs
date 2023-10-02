namespace GoldAggregator.Api.Dto
{
    public struct KeyLabelValuePair
    {
        public string Key { get; set; }
        public string Label { get; set; }
        public string Value { get; set; }

        public static KeyLabelValuePair Create(string key, string label, string value)
        {
            return new KeyLabelValuePair
            {
                Key = key,
                Label = label,
                Value = value
            };
        }
    }
}
