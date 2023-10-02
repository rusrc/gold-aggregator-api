using GoldAggregator.Parser.Dto;
using GoldAggregator.Parser.Entities.Entities;
using GoldAggregator.Parser.Provider.Attributes;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldAggregator.Parser.Provider.Realization
{
    [ProviderDescription(Host = "https://golddep.ru")]
    public class GoldDepProvider : BaseSiteProvider, ISiteParsingProvider
    {
        public Task<IEnumerable<CoinDto>> ParseAndGetItemsAsync(Url url)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Url>> ParseAndGetUrlsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
