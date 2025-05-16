using NumberParserExtended.Abstract;

namespace NumberParserExtended.Concrete
{
    /// <summary>
    /// Class represents an object used for parsing file line by line. 
    /// </summary>
    internal class NumberParserFileReader : INumberParserFileReader
    {
        /// <summary>
        /// Reads all the number lines from file.
        /// </summary>
        /// <param name="reader">Input File Stream.</param>
        /// <param name="numberLineHeigth">Heigth of a number line to be parsed (represented in text lines count).</param>
        /// <returns>Collection of <see cref="IParseLine"/> number line representations.</returns>
        public IEnumerable<IParseLine> ReadFileLines(StreamReader reader, int numberLineHeigth)
        {
            // Perfom a "pagination-like" approach to read string lines from file and form a "number lines", grouping them according to the line heigth.

            var fileLineIterator = 1;
            var fileLinesBuffer = new List<string>(numberLineHeigth);

            while (!reader.EndOfStream)
            {
                var currentLine = reader.ReadLine();

                if (string.IsNullOrWhiteSpace(currentLine))
                {
                    continue;
                }

                fileLinesBuffer.Add(currentLine);

                // In case the line heigth is reached, create a new IParseLine instance and start reading a new number line.
                if (fileLineIterator % numberLineHeigth == 0)
                {
                    yield return new ParseLine(fileLinesBuffer);
                    fileLinesBuffer.Clear();
                }

                fileLineIterator++;
            }
        }
    }
}
