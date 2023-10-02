using GoldAggregator.Parser.Exceptions;

using System;

namespace GoldAggregator.Parser.Provider.Exceptions
{
    public class UrlNotFoundException : NotFoundException
    {
        public UrlNotFoundException(string providerName) : base(providerName)
        {
        }
        public UrlNotFoundException(string providerName, string message) : base(providerName, message)
        {
        }

        public UrlNotFoundException(string providerName, string message, Exception innerException) : base(providerName, message, innerException)
        {
        }
    }
}
