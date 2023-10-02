using System.Collections.Generic;
using System.Threading.Tasks;

using GoldAggregator.Parser.Entities.Entities;
using GoldAggregator.Parser.Exceptions;
using GoldAggregator.Parser.Provider.Exceptions;
using GoldAggregator.Parser.Dto;
using System;

namespace GoldAggregator.Parser.Provider
{
    public interface ISiteParsingProvider
    {
        /// <summary>
        /// Get all links on target web-site
        /// to process them later in GetItem method
        /// <exception cref="NotFoundException"></exception>
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Url>> ParseAndGetUrlsAsync();
        //{
        //    // Один из вариантов реализации, в качестве черновика.

        //    // Можно оставить 0 т.к. большество сайтов понимают
        //    var pageNumber = 0;
        //    var isNextPage = false;

        //    do
        //    {
        //        // Пробигаем по странице в поисках существующих страниц
        //        // var html = null; Получим html страницу

        //        // Опредлеим есть ли нам следующая страница.
        //        // isNextPage = nextPageExists;
        //        pageNumber++;

        //    } while (isNextPage);


        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// Parse and get resulted items by url
        /// </summary>
        /// <param name="url"></param>
        /// <exception cref="NormalizedValueException"></exception>
        /// <exception cref="NotFoundException"></exception>
        /// <exception cref="ProviderException"></exception>
        /// <exception cref="RequiredFieldException"></exception>
        /// <exception cref="PriceNotFoundException"></exception>
        /// <exception cref="ItemSoldException"></exception>
        /// <returns></returns>
        Task<IEnumerable<CoinDto>> ParseAndGetItemsAsync(Url url);
    }
}
