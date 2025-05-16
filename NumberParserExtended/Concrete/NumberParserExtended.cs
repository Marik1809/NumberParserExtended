using NumberParserExtended.Abstract;

namespace NumberParserExtended.Concrete
{
    /// <summary>
    /// Class that represents the main Number Parser Object. 
    /// </summary>
    internal class NumberParserExtended : INumberParserExtended
    {
        private readonly INumberParserFileReader _fileReader;
        private readonly int _defaultLineHeigth;

        public NumberParserExtended()
            : this(new NumberParserFileReader(), NumberParserSettings.DefaultNumberParserSettings)
        { }

        public NumberParserExtended(INumberParserFileReader fileReader, NumberParserSettings parserSettings)
        {
            ArgumentNullException.ThrowIfNull(nameof(fileReader));
            ArgumentNullException.ThrowIfNull(nameof(parserSettings));

            _fileReader = fileReader;
            _defaultLineHeigth = parserSettings.DefaultParsingLineHeigth;
        }

        /// <summary>
        /// Method parces file specified into integer numbers represented line by line.
        /// </summary>
        /// <param name="filePath">Path to target file.</param>
        /// <param name="numberLineHeigth">Heigth of a number line to be parsed (represented in text lines count). In case the parameter is null, a default setting value will be used.</param>
        public IEnumerable<IEnumerable<int>> ParseNumbersFromFile(string filePath, int? numberLineHeigth = null)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(nameof(filePath));

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File not found: {filePath}");
            }

            using StreamReader streamReader = File.OpenText(filePath);

            foreach (var parsedLine in _fileReader.ReadFileLines(streamReader, numberLineHeigth ?? _defaultLineHeigth))
            {
                yield return parsedLine.ParseNumbers();
            }
        }
    }
}
