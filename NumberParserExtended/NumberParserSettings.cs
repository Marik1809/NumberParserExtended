namespace NumberParserExtended
{
    internal class NumberParserSettings
    {
        public int DefaultParsingLineHeigth { get; set; }
        public int MinimalParsingLineHeigth { get; set; }
        public char[] VerticalLinePossibleSymbols { get; set; } = [];
        public char[] HorizontalLinePossibleSymbols { get; set; } = [];

        public static NumberParserSettings DefaultNumberParserSettings { get; }
            = new NumberParserSettings
            {
                DefaultParsingLineHeigth = 4,
                VerticalLinePossibleSymbols = ['\\', '/', '|'],
                HorizontalLinePossibleSymbols = ['-', '_']
            };
    }
}
