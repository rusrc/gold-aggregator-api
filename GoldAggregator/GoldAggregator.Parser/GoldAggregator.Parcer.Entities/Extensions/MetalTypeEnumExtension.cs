using GoldAggregator.Parser.Entities.Enums;

namespace GoldAggregator.Parcer.Entities.Extensions
{
    public static class MetalTypeEnumExtension
    {
        public static string GetMetalName(this MetalTypeEnum metalType)
        {
            return metalType switch
            {
                MetalTypeEnum.Gold => "Золото",
                MetalTypeEnum.Silver => "Серебро",
                MetalTypeEnum.Platinum => "Платина",
                MetalTypeEnum.Palladium => "Палладий",
                _ => "Неопределенно",
            };
        }

        /// <summary>
        /// Пример:
        /// "Золотая монета"
        /// 
        /// Именительный:
        /// Прилагательные, причастия, порядковые числительные
        /// (какой? какая? какое? какие?)
        /// </summary>
        public static string GetMetalNounName(this MetalTypeEnum metalType)
        {
            return metalType switch
            {
                MetalTypeEnum.Gold => "золотая",
                MetalTypeEnum.Silver => "серебряная",
                MetalTypeEnum.Platinum => "платиновая",
                MetalTypeEnum.Palladium => "палладиевая",
                _ => "",
            };
        }

        /// <summary>
        /// Пример:
        /// "Золотую монета"
        /// 
        /// Родительный:
        /// (кого? что?)
        /// </summary>
        public static string GetMetalAccusativeName(this MetalTypeEnum metalType)
        {
            return metalType switch
            {
                MetalTypeEnum.Gold => "золотую",
                MetalTypeEnum.Silver => "серебряную",
                MetalTypeEnum.Platinum => "платиновую",
                MetalTypeEnum.Palladium => "палладиевую",
                _ => "",
            };
        }

        public static string GetMetalPluralNounName(this MetalTypeEnum metalType)
        {
            return metalType switch
            {
                MetalTypeEnum.Gold => "золотые",
                MetalTypeEnum.Silver => "серебряные",
                MetalTypeEnum.Platinum => "платиновые",
                MetalTypeEnum.Palladium => "палладиевые",
                _ => "",
            };
        }
    }
}
