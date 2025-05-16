namespace NumberParserExtended.Abstract
{
    /// <summary>
    /// Interface that represents the main Number Parser Object. 
    /// </summary>
    internal interface INumberParserExtended
    {
        /// <summary>
        /// Method parces file specified into integer numbers represented line by line.
        /// </summary>
        /// <param name="filePath">Path to target file.</param>
        /// <param name="numberLineHeigth">Heigth of a number line to be parsed (represented in text lines count). In case the parameter is null, a default setting value will be used.</param>
        IEnumerable<IEnumerable<int>> ParseNumbersFromFile(string filePath, int? numberLineHeigth = null);
    }
}
