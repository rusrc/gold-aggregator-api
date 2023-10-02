using GoldAggregator.Api.Dto;

using Schema.NET;

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace GoldAggregator.Parser.Services
{
    /// <summary>
    /// https://validator.schema.org/
    /// 
    /// https://github.com/RehanSaeed/Schema.NET
    /// </summary>
    public class JsonLdService
    {
        public object HomePage()
        {
            var website = new WebSite
            {
                AlternateName = "Голд Каталог",
                Name = "Gold Katalog",
                Url = new Uri("https://goldkatalog.ru/"),
                About = new Thing
                {
                    Description = "Ресурс где вы сможете найти лучшие цена в городе, чтобы купить или продать: " +
                        "золотые  слитки, золотые, серебряные, платиновые или коллекционные монеты; " +
                        "найдите у нас выгодный ломбард в своем или ближайшем городе."
                },
                Image = new Uri("https://goldkatalog.ru/assets/icons/logo-small.png?v=20220706-074747220?v=20220706-074747220"),

            };

            var jsonLd = website;

            return jsonLd;
        }

        // TODO
        public object HomeCoinsPageByFilters(string title, string description, IEnumerable<CoinCatalogDto> coinCatalogDtos)
        {
            var listItems = new List<IListItem>();

            int position = 1;
            foreach (var coinCatalogDto in coinCatalogDtos)
            {
                listItems.Add(new ListItem
                {
                    Position = position,
                    Name = coinCatalogDto.Name,
                    Description = $"{coinCatalogDto.MetalTypeName} монета '{coinCatalogDto.Name}'",
                    Image = new ImageObject { Url = new Uri($"https://goldkatalog.ru/assets/coins/{coinCatalogDto.ImageName}_reverse.webp") }
                });

                position++;
            }

            var itemList = new ItemList
            {
                Name = title,
                Description = description,
                ItemListElement = listItems
            };

            return itemList;
        }

        public object CoinCatalogPage(string title, string description, CoinCatalogDto coinCatalog)
        {
            var product = new Product
            {
                Name = coinCatalog.Name,
                Description = description,
                Material = coinCatalog.MetalTypeName,
                // TODO get root image value from configuration
                Image = new Uri($"https://goldkatalog.ru/assets/coins/{coinCatalog.ImageName}_obverse.webp"),

                // TODO several prices
                Offers = new Offer
                {
                    PriceSpecification = new PriceSpecification
                    {
                        MinPrice = coinCatalog.MinPriceToSell,
                        MaxPrice = coinCatalog.MaxPriceToSell,
                        Description = "Диапазан цена для покупки монеты"
                    },
                    PriceCurrency = "RUB"
                },

                Weight = new QuantitativeValue
                {
                    Description = "вес в граммах",
                    Value = coinCatalog.Weight.ToString(),
                    Name = "вес",
                },
                
                
            };

            return product;
        }
    }
}

