using GoldAggregator.Parser.Provider;

using System;

using Xunit;

namespace GoldAggregator.Parser.Tests
{
    public class TestBaseSiteProvider
    {
        private BaseSiteProviderToTest _baseProvider = new();

        [Theory]
        [InlineData("2022", "2022")]
        [InlineData("Золотая монета Камеруна \"Вепрь\" 2022 г.в., 7.78 г чистого золота (Проба 0,9999)", "2022")]
        [InlineData("Золотая Георгий Победоносец СПМД 2018-2022 г.в., 7.78 г чистого золота (проба 0,999)", "2018")]
        [InlineData("Золотая Георгий Победоносец СПМД 1975 - 2022 г., 7.78 г чистого золота (проба 0,999)", "1975")]
        [InlineData("Инвестиционная золотая монета Чемпионат мира по футболу FIFA 2018 года 50 рублей (7,78 гр проба 0, 999)", "2018")]
        [InlineData("Золотой червонец Сеятель, 1976,1977,1979,1980,1981  гг.в.,  вес чистого золота - 7.742 г (проба 0.900)", "1976")] // -
        [InlineData("Серебряная монета Австралии \"Лунный календарь III - Год Тигра\" 2022 г.(пруф с цветом), 31.1 г чистого серебра (Проба 0,9999)", "2022")]
        [InlineData("Золотая монета Камеруна \"Вепрь\" 2022 г. , 7.78 г чистого золота (Проба 0,9999)", "2022")]
        [InlineData("Золотая инвестиционная монета Австрии Венский Филармоникер, 31,1 гр чистого золота (проба 0,9999)", null)]
        public void InputMultilineText_MatchStartYear_GetStartingYearFromText(string text, string expected)
        {
            string year = _baseProvider.GetStartingYearFromText(text);

            Assert.Equal(expected, year);
        }

        [Theory]
        [InlineData("2022", "2022")]
        [InlineData(" - 2022", "2022")]
        [InlineData("Золотая монета Камеруна \"Вепрь\" 2022 г.в., 7.78 г чистого золота (Проба 0,9999)", "2022")]
        [InlineData("Золотая Георгий Победоносец СПМД 2018-2022 г.в., 7.78 г чистого золота (проба 0,999)", "2022")]
        [InlineData("Золотая Георгий Победоносец СПМД 1975 - 2022 г., 7.78 г чистого золота (проба 0,999)", "2022")]
        [InlineData("Инвестиционная золотая монета Чемпионат мира по футболу FIFA 2018 года 50 рублей (7,78 гр проба 0, 999)", "2018")]
        [InlineData("Золотой червонец Сеятель, 1976,1977,1979,1980,1981  гг.в.,  вес чистого золота - 7.742 г (проба 0.900)", "1981")] // -
        [InlineData("Серебряная монета Австралии \"Лунный календарь III - Год Тигра\" 2022 г.(пруф с цветом), 31.1 г чистого серебра (Проба 0,9999)", "2022")]
        [InlineData("Золотая монета Камеруна \"Вепрь\" 2022 г. , 7.78 г чистого золота (Проба 0,9999)", "2022")]
        [InlineData("Золотая инвестиционная монета Австрии Венский Филармоникер, 31,1 гр чистого золота (проба 0,9999)", null)]
        public void InputMultilineText_MatchEndYear_GetEndingYearFromText(string text, string expected)
        {
            string year = _baseProvider.GetEndingYearFromText(text);

            Assert.Equal(expected, year);
        }

        [Theory]
        //[InlineData("Золотая инвестиционная монета Австрии Венский Филармоникер, 31,1 гр чистого золота (проба 0,9999)", "31,1", "гр")]
        //[InlineData("Золотая монета Камеруна \"Вепрь\" 2022 г.в., 7.78 г чистого золота (Проба 0,9999)", "7.78", "г")]
        //[InlineData("Золотой червонец Сеятель, 1976,1977,1979,1980,1981  гг.в.,  вес чистого золота - 7.742 г (проба 0.900)", "7.742", "г")]

        [InlineData("1г", "1")]
        [InlineData("30г", "30")]
        [InlineData("62.2г", "62.2")]

        [InlineData("1гр", "1")]
        [InlineData("30гр", "30")]
        [InlineData("62.2гр", "62.2")]
        public void InputMultilineText_MatchWeight_GetWeightFromText(string text, string expectedWeight)
        {
            var r = _baseProvider.GetWeightFromText(text);

            Assert.Equal(expectedWeight, r.Weight);
        }

        [Theory]
        [InlineData("142", 142)]
        [InlineData(" 142", 142)]
        [InlineData(" 142 ", 142)]
        [InlineData(" 14 2 ", 142)]

        [InlineData("1,42", 1.42)]
        [InlineData(" 1,42", 1.42)]
        [InlineData(" 1,42 ", 1.42)]

        [InlineData("1.42", 1.42)]
        [InlineData(" 1.42", 1.42)]
        [InlineData(" 1.42 ", 1.42)]

        [InlineData("Покупаем:14900₽", 14900)]
        [InlineData("Покупаем:14900 ₽", 14900)]
        [InlineData("Покупаем: 14900₽", 14900)]
        [InlineData("Покупаем: 14900 ₽", 14900)]

        [InlineData("Покупаем: 14,900₽", 14.900)]
        [InlineData("Покупаем: 14,900 ₽", 14.900)]
        [InlineData("Покупаем: 14,900₽", 14.900)]
        [InlineData("Покупаем: 14,900 ₽", 14.900)]

        [InlineData("Покупаем:14,900₽", 14.900)]
        [InlineData("Покупаем: 14.900 ₽", 14.900)]
        [InlineData("Покупаем: 14.900₽", 14.900)]
        [InlineData("Покупаем: 14.900 ₽", 14.900)]
        public void InputMultilineText_MatchPrice_GetPriceFromText(string text, decimal expected)
        {
            var price = _baseProvider.GetPriceFromText(text);

            Assert.Equal(expected, price);
        }
    }

    public class BaseSiteProviderToTest : BaseSiteProvider
    {
        public string GetStartingYearFromText(string text)
        {
            return base.GetStartingYearFromText(text);
        }

        public string GetEndingYearFromText(string text)
        {
            return base.GetEndingYearFromText(text);
        }

        public decimal GetPriceFromText(string text)
        {
            return base.GetPriceFromText(text);
        }

        public (bool Result, string Weight) GetWeightFromText(string text)
        {
            var result = base.GetWeightFromText(text, out string weight);

            return (result, weight);
        }
    }
}