namespace NumberParserExtended.Abstract
{
    /// <summary>
    /// Interface represents raw symbolic numbers line prepared for parsing.
    /// </summary>
    internal interface IParseLine
    {
        /// <summary>
        /// Method returns parsed interpreted numbers from the line.
        /// </summary>
        IEnumerable<int> ParseNumbers();
    }
}
