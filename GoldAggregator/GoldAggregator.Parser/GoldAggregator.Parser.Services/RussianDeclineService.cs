using Cyriller;

using GoldAggregator.Parser.Services.Abstractions;

namespace GoldAggregator.Parser.Services
{
    // !!!!!!!!!! ВНИМАНИЕ !!!!!!!!!! Сервис долго инстанцируется,
    // поэтому либо надо через singletone либо альтернативный глянуть.
    // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    /// <summary>
    /// Used https://github.com/miyconst/Cyriller/blob/master/Cyriller.Samples/Program.cs#L48
    /// </summary>
    public class RussianDeclineService : IDeclineService
    {
        private readonly CyrNounCollection _nouns;
        private readonly CyrAdjectiveCollection _adjectives;

        public RussianDeclineService()
        {
            _nouns = new CyrNounCollection();
            // _adjectives = new CyrAdjectiveCollection();
        }

        /// <inheritdoc />
        public string GetNominative(string noun)
        {
            try
            {
                return _nouns.Get(noun, out _, out _).Decline().Nominative;
            }
            catch
            {
                // log error
                return noun;
            }
        }

        /// <inheritdoc />
        public string GetGenitive(string noun)
        {
            try
            {
                return _nouns.Get(noun, out _, out _).Decline().Genitive;
            }
            catch
            {
                // log error
                return noun;
            }
        }

        /// <inheritdoc />
        public string GetDative(string noun)
        {       
            try
            {
                return _nouns.Get(noun, out _, out _).Decline().Dative;
            }
            catch
            {
                // log error
                return noun;
            }
        }

        /// <inheritdoc />
        public string GetPrepositional(string noun)
        {
            try
            {
                return _nouns.Get(noun, out _, out _).Decline().Prepositional;
            }
            catch
            {
                // log error
                return noun;
            }
        }

        /// <inheritdoc />
        public string GetAccusative(string noun)
        { 
            try
            {
                return _nouns.Get(noun, out _, out _).Decline().Accusative;
            }
            catch
            {
                // log error
                return noun;
            }
        }

        /// <inheritdoc />
        public string GetInstrumental(string noun)
        {
            try
            {
                return _nouns.Get(noun, out _, out _).Decline().Instrumental;
            }
            catch
            {
                // log error
                return noun;
            }
        }

        // ---------- Plural ----------------------

        /// <inheritdoc />
        public string GetNominativePlural(string noun)
        {
            var result = _nouns
                .Get(noun, out _, out _)
                .DeclinePlural()
                .Nominative;

            return result;
        }

    }
}
