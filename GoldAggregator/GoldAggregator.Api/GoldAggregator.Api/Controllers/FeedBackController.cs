using GoldAggregator.Api.Dto;
using GoldAggregator.Parser.Services;

using Microsoft.AspNetCore.Mvc;

using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace GoldAggregator.Api.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class FeedBackController : ControllerBase
    {
        private readonly ILogger<FeedBackController> _logger;
        private readonly GrecaptchaService _grecaptchaService;
        private readonly TelegramService _telegramService;

        public FeedBackController(ILogger<FeedBackController> logger, GrecaptchaService grecaptchaService, TelegramService telegramService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _grecaptchaService = grecaptchaService ?? throw new ArgumentNullException(nameof(grecaptchaService));
            _telegramService = telegramService ?? throw new ArgumentNullException(nameof(telegramService));
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Check(DtoFeedBackRequest request)
        {
            var response = await _grecaptchaService.CheckAsync(request.GrecaptchaToken);

            if (response.Success)
            {
                var mappedResult = new
                {
                    request.Phone,
                    request.Email,
                    request.Comment,
                    request.Agreement
                };


                var options = new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                    WriteIndented = true
                };
                var message = JsonSerializer.Serialize(mappedResult, options);
                await this._telegramService.Publish(message);

                return Ok();
            }

            return BadRequest();
        }
    }
}
