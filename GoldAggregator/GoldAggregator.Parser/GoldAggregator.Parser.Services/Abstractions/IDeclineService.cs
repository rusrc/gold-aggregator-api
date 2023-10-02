namespace GoldAggregator.Parser.Services.Abstractions
{
    public interface IDeclineService
    {
        /// <summary>
        /// Иминительный (кто? что?)
        /// <example>
        ///     Москва
        /// </example>
        /// </summary>
        string GetNominative(string noun);

        /// <summary>
        /// Родительный (кого? чего?)
        /// <example>
        ///    Москвы
        /// </example>
        /// </summary>
        string GetGenitive(string noun);

        /// <summary>
        /// Дательный (кому? чему?)
        /// <example>
        ///     Москве, Санкт-Петербурге
        /// </example>
        /// </summary>
        string GetDative(string noun);

        /// <summary>
        /// Винительный (кого? что?)
        /// <example>
        ///     Москву, Санкт-Петербургу
        /// </example>
        /// </summary>
        string GetAccusative(string noun);

        /// <summary>
        /// Творительный (кем? чем?)
        /// <example>
        ///     Москвой
        /// </example>
        /// </summary>
        string GetInstrumental(string noun);

        /// <summary>
        /// Prepositional
        /// Предложный (о ком? о чём?)
        /// <example>
        ///     Москве, Санкт-Петербурге
        /// </example>
        /// </summary>
        string GetPrepositional(string noun);


        // ---------- Plural ----------------------
        string GetNominativePlural(string noun);
    }
}
