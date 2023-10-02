namespace GoldAggregator.Parser.Entities.Enums
{
    public enum Status
    {
        Undefined = 0,
        /// <summary>
        /// "Active" when added successfully and waiting to update 
        /// </summary>
        Active = 1,
        /// <summary>
        /// Parsed successfully and ready to be parsed
        /// </summary>
        ReadyToParse = 2,
        /// <summary>
        /// "Wait to check" when was thrown expected exception and wating to read what's going on
        /// </summary>
        WaitToCheck = 3,
        /// <summary>
        /// "Not found" sold or returns not found status code
        /// </summary>
        NotFound = 4,
        /// <summary>
        /// Return error
        /// </summary>
        Error = 5
    }
}
