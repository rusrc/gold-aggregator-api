using AutoMapper;

using GoldAggregator.Api.Dto;
using GoldAggregator.Infrastructure.Repositories;
using GoldAggregator.Parser.Entities.Entities;

using Microsoft.AspNetCore.Mvc;

namespace GoldAggregator.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CitiesController : ControllerBase
    {
        private readonly ILogger<CitiesController> _logger;
        private readonly IMapper _mapper;
        private readonly ICitiesRepository _citiesRepository;

        public CitiesController(
            ILogger<CitiesController> logger,
            IMapper mapper,
            ICitiesRepository citiesRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _citiesRepository = citiesRepository ?? throw new ArgumentNullException(nameof(citiesRepository));
        }

        /// <summary>
        /// Все города
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetCities()
        {
            var cities = await _citiesRepository.GetCitiesAsync();
            var dtoCities = _mapper.Map<IEnumerable<City>, IEnumerable<DtoCity>>(cities);

            return Ok(dtoCities);
        }
    }
}
