using NumberParserExtended.Abstract;

namespace NumberParserExtended
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // For demo purposes only. Possible TODO: Handle all possible failures and incorrect cases.
            // Read all .txt files from the current working directory, parse them and print results line-by-line for each file.

            var currentDirectory = Directory.GetCurrentDirectory();
            Console.WriteLine($"Working directory: {currentDirectory}");

            INumberParserExtended parser = new Concrete.NumberParserExtended();

            foreach (var file in Directory.GetFiles(currentDirectory).Where(f => f.EndsWith(".txt")))
            {

                Console.WriteLine($"Parsing file {file}");

                try
                {

                    var customNumberlineHeigth = TryGetCustomNumberLinesHeigth(file);

                    if (customNumberlineHeigth.HasValue)
                    {
                        Console.WriteLine($"Custom number line heigth retrieved from file name: {customNumberlineHeigth.Value}");
                    }

                    // If the file name contains custom height for the number line, pass the heigth to the parser. Otherwise, value from the settings will be used.
                    var parsedContent = customNumberlineHeigth.HasValue
                        ? parser.ParseNumbersFromFile(file, customNumberlineHeigth.Value)
                        : parser.ParseNumbersFromFile(file);

                    Console.WriteLine($"Parsed results line-by-line:");

                    foreach (var line in parsedContent)
                    {
                        foreach (var item in line)
                        {
                            Console.Write(item);
                        }

                        Console.WriteLine();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"File parsing failed. Details: {ex.ToString()}");
                }

                Console.WriteLine();
            }

            Console.WriteLine("Press any key to proceed...");
            Console.ReadKey();

        }

        // Retrieve custom number height in lines from the file name.
        // For demo purposes only. Possible TODO: Handle all possible failures and incorrect cases.
        // To adjust custom number line heigth for the file - add (Number_n_Lines_High) to the file name. Where n - int number representing the desired height.
        static int? TryGetCustomNumberLinesHeigth(string fileName)
        {
            var templatePrefix = "(Number_";
            var templateSuffix = "_Lines_High)";

            if (fileName.Contains(templatePrefix) && fileName.Contains(templateSuffix))
            {
                var customHeigthStartPositionIndex = fileName.LastIndexOf(templatePrefix) + templatePrefix.Length;
                var customHeigthLength = fileName.LastIndexOf(templateSuffix) - customHeigthStartPositionIndex;

                return int.TryParse(fileName.Substring(customHeigthStartPositionIndex, customHeigthLength), out var parsed)
                    ? parsed : null;
            }

            return null;
        }
    }
}

