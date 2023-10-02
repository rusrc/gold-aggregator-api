using GoldAggregator.Api.Dto;
using GoldAggregator.Infrastructure.Repositories;
using GoldAggregator.Parser.DbContext;
using GoldAggregator.Parser.Logger;

using Microsoft.AspNetCore.Mvc;

namespace GoldAggregator.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FiltersController : ControllerBase
    {
        private readonly ParserDbContext _context;
        private readonly ILogger<FiltersController> _logger;
        private readonly IFiltersRepository _filtersRepository;

        public FiltersController(
            ParserDbContext context,
            ILogger<FiltersController> logger,
            IFiltersRepository filterRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _filtersRepository = filterRepository ?? throw new ArgumentNullException(nameof(filterRepository));
        }

        /// <summary>
        /// Действующие фильтры для получения монет <see cref="CoinsPriceController"/>
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(KeyLabelValuePair[]))]
        public async Task<IActionResult> GetFilters()
        {
            using var t = new TimePointer<FiltersController>(nameof(GetFilters), _logger);
            var filters = await _filtersRepository.GetFiltersAsync();

            return Ok(filters);
        }
    }
}
