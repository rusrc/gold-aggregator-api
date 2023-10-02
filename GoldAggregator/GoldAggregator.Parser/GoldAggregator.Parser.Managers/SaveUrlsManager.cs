using GoldAggregator.Parser.DbContext;
using GoldAggregator.Parser.Entities.Entities;
using GoldAggregator.Parser.Entities.Enums;
using GoldAggregator.Parser.Exceptions;
using GoldAggregator.Parser.Provider;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GoldAggregator.Parser.Managers
{
    public class SaveUrlsManager : ISaveUrlsManager
    {
        private readonly ISiteParsingProvider _siteProvider;
        private readonly ILogger<SaveUrlsManager> _logger;
        private readonly ParserDbContext _context;

        public SaveUrlsManager(
            ISiteParsingProvider siteProvider,
            ILogger<SaveUrlsManager> logger,
            ParserDbContext context)
        {
            _siteProvider = siteProvider;
            _logger = logger;
            _context = context;
        }

        public async Task RunAsync(CancellationToken cancellationToken)
        {
            try
            {
                var providerName = _siteProvider.GetType().Name;
                var urls = await _siteProvider.ParseAndGetUrlsAsync();
                var now = DateTime.Now;
                var dealer = _context.Dealers.First(d=>d.ProviderName == providerName);
                var linksFromDb = _context.Urls.Where(l => l.DealerId == dealer.Id).ToArray();
                                
                foreach (var url in urls)
                {
                    // Don't add excisted
                    if (linksFromDb.All(l => l.Value != url.Value))
                    {
                        _context.Urls.Add(new Url
                        {
                            Value = url.Value,
                            CreateDate = now,  
                            DealerId = dealer.Id,
                            Status = Status.ReadyToParse
                        });
                    }
                }

               var results = await _context.SaveChangesAsync();

                if (results > 0)
                    _logger.LogInformation("Urls added succeeded");
                else
                    _logger.LogInformation("Urls check succeeded, no urls");

            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex.Message, ex);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message, ex);
            }
        }
    }
}
