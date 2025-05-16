using NumberParserExtended.Abstract;

namespace NumberParserExtended.Concrete
{
    /// <summary>
    /// Class represents raw symbolic numbers line prepared for parsing.
    /// Implemented as raw symbolic "matrix", that is iterated column-by-column to perform number interpretation.
    /// </summary>
    internal class ParseLine : IParseLine
    {
        private readonly char[][] _lineBuffer;
        private readonly INumberInterpreter _numberInterpreter;

        public ParseLine(IEnumerable<string> inputLines)
            : this(inputLines, new NumberInterpreter())
        { }

        public ParseLine(IEnumerable<string> inputLines, INumberInterpreter numberInterpreter)
        {
            ArgumentNullException.ThrowIfNull(nameof(numberInterpreter));
            ArgumentNullException.ThrowIfNull(nameof(inputLines));

            _numberInterpreter = numberInterpreter;
            var inputLinesCount = inputLines.Count();

            if (inputLinesCount == 0)
            {
                _lineBuffer = [];
            }
            else
            {

                // Transform input strings into a raw symbolic "matrix" - a jagged array grouped by columns.

                // Example:
                // Line 1: " --- "
                // Line 2: "|   |"
                // Line 3: "|   |"
                // Line 4: " --- "
                //
                // Transprmed into
                //      |
                //      V
                // Columns: [1] [2] [3] [4] [5]
                //           |   |   |   |   |
                //  Rows:    V   V   V   V   V
                //    [1]   ' ' '-' '-' '-' ' '
                //    [2]   '|' ' ' ' ' ' ' '|'
                //    [3]   '|' ' ' ' ' ' ' '|'
                //    [4]   ' ' '-' '-' '-' ' '

                var lineWidth = inputLines.Max(line => line.Length);

                _lineBuffer = new char[lineWidth][];

                for (int i = 0; i < lineWidth; i++)
                {
                    _lineBuffer[i] = new char[inputLinesCount];

                    for (int j = 0; j < inputLinesCount; j++)
                    {
                        _lineBuffer[i][j]
                            = inputLines
                                .ElementAt(j)
                                .ElementAtOrDefault(i);
                    }
                }
            }
        }

        /// <summary>
        /// Method returns parsed interpreted numbers from the line.
        /// </summary>
        public IEnumerable<int> ParseNumbers()
        {
            // Blank column is a marker to start a new number interpretation.
            static bool IsBlankColumn(char[] column)
                => column.All(item => item is default(char) || char.IsWhiteSpace(item));

            for (int i = 0; i < _lineBuffer.Length; i++)
            {
                
                if (IsBlankColumn(_lineBuffer[i]) && _numberInterpreter.ContainsState)
                {
                    // Once a blank column is detected, try to interpret the actual number from the internal interpreted state persisted during previous iterations.
                    var (sucess, output) = _numberInterpreter.TryInterpret();

                    if (sucess) yield return output.Value;

                    // Reset internal interpretation state for the next interpretation.
                    _numberInterpreter.Reset();
                }
                else
                {
                    // "Feed" every "not empty" column to the interpreter.
                    _numberInterpreter.ProcessLineColumn(_lineBuffer[i]);

                    // Special case: at the end of the line try to interpret current pesisted interpretation state if exists.
                    if ((i == _lineBuffer.Length - 1) && _numberInterpreter.ContainsState)
                    {
                        var (sucess, output) = _numberInterpreter.TryInterpret();

                        if (sucess) yield return output.Value;

                        _numberInterpreter.Reset();
                    }
                }
            }
        }
    }
}
