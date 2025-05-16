NumberParserExtended - a test console app dedicated to parse "dash- and slash-" coded numbers from .txt files to int representation. The result is printed to the console output.

For simplicity purposes, the App takes no console parameters. It looks through the working directory (location of the app executable) for all .txt files present.
Every .txt file is parsed to numbers and the result is printed to the console output file-by-lile and then line-by-line.

To run the app:

- Build the solution
- Copy all test .txt files to the build output directory if they do not appear there after build. (I had some issues with related VisualStudio settings for copying them during the build).
- Execute the app.

Important notes about the .txt files:

- Every number should be of the same height in scope of file. The App will attempt to read the file line-by-line
- Default heigth can be adjusted using the settings functionality in the app.
- Additionally (for demo purposes only) one can set the specific line heigth for specific file. (NumberParserExtended(Number_4_Lines_High).txt and NumberParserExtended(Number_5_Lines_High).txt files are used to test different custom-file-set line heigth)
- .txt files should not contain tabulation (/t) characters. Otherwise the current interpretation will most likely fail.
