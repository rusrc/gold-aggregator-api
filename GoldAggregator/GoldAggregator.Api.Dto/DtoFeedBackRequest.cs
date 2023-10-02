namespace GoldAggregator.Api.Dto
{
    public class DtoFeedBackRequest
    {
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
        public bool Agreement { get; set; }
        public string GrecaptchaToken { get; set; }
    }
}
