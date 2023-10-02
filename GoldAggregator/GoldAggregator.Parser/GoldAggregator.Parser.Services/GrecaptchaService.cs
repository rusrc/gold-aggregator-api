using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace GoldAggregator.Parser.Services
{
    public class GrecaptchaService
    {
        public const string SECRET_KEY = "6LdN1cUgAAAAAOpgbfsikpt8WowTDP-8PWpqr-YX";
        public const string GRECAPTCHA_URL = "https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}";

        /// <summary>
        /// Совершает запрос на проверку по ссылке https://www.google.com/recaptcha/api/siteverify
        /// </summary>
        /// <param name="grecaptchaResponse">Хеш-код гуглкапчи полученный от клиента</param>
        /// <returns>Grecaptcha</returns>
        public async Task<GrecaptchaResponse> CheckAsync(string grecaptchaResponse)
        {
            using var client = new WebClient();

            var result = await client.DownloadStringTaskAsync(string.Format(GRECAPTCHA_URL, SECRET_KEY, grecaptchaResponse));
            var response = JsonSerializer.Deserialize<GrecaptchaResponse>(result, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return response;
        }

        #region Helper
        private string GetError(string errorName)
        {
            var msg = string.Empty;

            switch (errorName)
            {
                case ("missing-input-secret"):
                    msg = "";// ResxCabinet.gRecaptchaMissingInputSecret;
                    break;
                case ("invalid-input-secret"):
                    msg = ""; // ResxCabinet.gRecaptchaInvalidInputSecret;
                    break;
                case ("missing-input-response"):
                    msg = ""; // ResxCabinet.gRecaptchaMissingInputResponse;
                    break;
                case ("invalid-input-response"):
                    msg = ""; // ResxCabinet.gRecaptchaInvalidInputResponse;
                    break;
                default:
                    msg = ""; // ResxCabinet.gRecaptchaDefault;
                    break;
            }

            return msg;

        }
        #endregion

        public class GrecaptchaResponse
        {
            // [JsonProperty("success")]
            public bool Success { get; set; }

            // [JsonProperty("error-codes")]
            public List<string> ErrorCodes { get; set; }
        }
    }
}
