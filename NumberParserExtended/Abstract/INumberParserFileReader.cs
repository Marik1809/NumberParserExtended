namespace NumberParserExtended.Abstract
{
    /// <summary>
    /// Interface represents an object used for parsing file line by line. 
    /// </summary>
    internal interface INumberParserFileReader
    {
        /// <summary>
        /// Reads all the number lines from file.
        /// </summary>
        /// <param name="reader">Input File Stream.</param>
        /// <param name="numberLineHeigth">Heigth of a number line to be parsed (represented in text lines count).</param>
        /// <returns>Collection of <see cref="IParseLine"/> number line representations.</returns>
        IEnumerable<IParseLine> ReadFileLines(StreamReader reader, int numberLineHeigth);
    }
}
