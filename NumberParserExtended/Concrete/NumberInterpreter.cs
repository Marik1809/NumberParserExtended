using NumberParserExtended.Abstract;

namespace NumberParserExtended.Concrete
{
    /// <summary>
    /// Class representing the Number Interpreter, that "consumes" raw symbolic data column by column and preserves number detection state. 
    /// </summary>
    internal class NumberInterpreter : INumberInterpreter
    {
        // Interpretation number masks templates.
        private static readonly Dictionary<int, INumberMask[]> _interpretationTemplates = new Dictionary<int, INumberMask[]>
        {
            [0] =
                [
                    //  ---
                    // |   |
                    // |   |
                    //  ---
                    new NumberMask()
                            .WithTopHorizontalLine()
                            .WithTopRightVerticalLine()
                            .WithBottomRightVerticalLine()
                            .WithBottomHorizontalLine()
                            .WithBottomLeftVerticalLine()
                            .WithTopLeftVerticalLine()
                ],
            [1] =
                [
                    // |
                    // |
                    // |
                    // |
                    new NumberMask()
                            .WithBottomLeftVerticalLine()
                            .WithTopLeftVerticalLine(),
                ],

            [2] =
                [
                    //  ---
                    //  ___|
                    // |
                    //  ---
                    new NumberMask()
                            .WithTopHorizontalLine()
                            .WithTopRightVerticalLine()
                            .WithCenterHorizontalLine()
                            .WithBottomLeftVerticalLine()
                            .WithBottomHorizontalLine()
                ],
            [3] =
                [
                    //  ---
                    //  ___|
                    //     |
                    //  ---
                    new NumberMask()
                            .WithTopHorizontalLine()
                            .WithTopRightVerticalLine()
                            .WithCenterHorizontalLine()
                            .WithBottomRightVerticalLine()
                            .WithBottomHorizontalLine(),
                    //  ---
                    //    /
                    //    \
                    //  ---
                    // Special case of number 3 (provided in the example file).
                    new NumberMask()
                            .WithTopHorizontalLine()
                            .WithTopRightVerticalLine()
                            .WithBottomRightVerticalLine()
                            .WithBottomHorizontalLine(),
                    ],
            [4] =
                [
                    // |   |
                    // |___|
                    //     |
                    //     |
                    new NumberMask()
                            .WithTopLeftVerticalLine()
                            .WithCenterHorizontalLine()
                            .WithTopRightVerticalLine()
                            .WithBottomRightVerticalLine()
                ],
            [5] =
                [
                    //  ---
                    // |___
                    //     |
                    //  ---
                     new NumberMask()
                            .WithTopHorizontalLine()
                            .WithTopLeftVerticalLine()
                            .WithCenterHorizontalLine()
                            .WithBottomRightVerticalLine()
                            .WithBottomHorizontalLine()
                ],
            [6] =
                [
                    //  ---
                    // |___
                    // |   |
                    //  ---
                    new NumberMask()
                            .WithTopHorizontalLine()
                            .WithTopLeftVerticalLine()
                            .WithCenterHorizontalLine()
                            .WithBottomRightVerticalLine()
                            .WithBottomLeftVerticalLine()
                            .WithBottomHorizontalLine()
                ],
            [7] =
                [
                    //  ---
                    //     |
                    //     |
                    //     |
                    //     |
                    new NumberMask()
                            .WithTopHorizontalLine()
                            .WithTopRightVerticalLine()
                            .WithBottomRightVerticalLine()
                ],
            [8] =
                [
                    //  ---
                    // |___|
                    // |   |
                    //  ---
                    new NumberMask()
                            .WithAllLines()
                ],
            [9] =
                [
                    //  ---
                    // |___|
                    //     |
                    //  ---
                    new NumberMask()
                            .WithTopHorizontalLine()
                            .WithTopLeftVerticalLine()
                            .WithTopRightVerticalLine()
                            .WithCenterHorizontalLine()
                            .WithBottomRightVerticalLine()
                            .WithBottomHorizontalLine()
                ],
        };

        private readonly char[] _verticalLinePossibleSymbols;
        private readonly char[] _horizontalLinePossibleSymbols;

        private INumberMask _numberMask;


        /// <summary>
        /// Returns true if there is current detection state saved in the current interpreter.
        /// </summary>
        public bool ContainsState => _numberMask.ContainsState;

        public NumberInterpreter()
            : this(new NumberMask(), NumberParserSettings.DefaultNumberParserSettings)
        { }

        public NumberInterpreter(INumberMask numberMask, NumberParserSettings numberParserSettings)
        {
            ArgumentNullException.ThrowIfNull(numberMask);
            ArgumentNullException.ThrowIfNull(numberParserSettings);

            _numberMask = numberMask;
            _verticalLinePossibleSymbols = numberParserSettings.VerticalLinePossibleSymbols ?? [];
            _horizontalLinePossibleSymbols = numberParserSettings.HorizontalLinePossibleSymbols ?? [];
        }

        /// <summary>
        /// Method takes a new raw symbolic column as the input, performs a part of number interpretation and updates the internal state of the current Interpreter.
        /// </summary>
        /// <param name="column">Raw symbolic input column for interpretetation.</param>
        public INumberInterpreter ProcessLineColumn(char[] column)
        {
            ArgumentNullException.ThrowIfNull(column);

            // Get the central vertical position index. Crucial for top/bottom/central lines detection.
            var centralVerticalIndex = GetCentralPositionIndex(column.Length);

            // Check current column and detect relative vertical positions of horizontal and vertical lines.
            var topVerticalLineDetected = false;
            var bottomVerticalLineDetected = false;
            var topHorizontalLineDetected = false;
            var centerHorizontalLineDetected = false;
            var bottomHorizontalLineDetected = false;

            for (int i = 0; i < column.Length; i++)
            {
                if (_verticalLinePossibleSymbols.Contains(column[i]))
                {
                    if (i <= centralVerticalIndex)
                    {
                        topVerticalLineDetected = true;
                    }
                    else if (i > centralVerticalIndex)
                    {
                        bottomVerticalLineDetected = true;
                    }
                }

                if (_horizontalLinePossibleSymbols.Contains(column[i]))
                {
                    if (i < centralVerticalIndex)
                    {
                        topHorizontalLineDetected = true;
                    }
                    else if (i > centralVerticalIndex)
                    {
                        bottomHorizontalLineDetected = true;
                    }
                    else
                    {
                        centerHorizontalLineDetected = true;
                    }
                }
            }

            // Based on detection, proceed with internal number mask state update.

            // Vertical detections should be processed first, to access the previous horizontal detections.
            // If internal state contains horizontal lines detected earlier, most likely the current vertical would be a right part detection.

            if (topVerticalLineDetected)
            {
                _numberMask = AnyHorizontalLinesPresentInMask()
                    ? _numberMask.WithTopRightVerticalLine()
                    : _numberMask.WithTopLeftVerticalLine();
            }

            if (bottomVerticalLineDetected)
            {
                _numberMask = AnyHorizontalLinesPresentInMask()
                    ? _numberMask.WithBottomRightVerticalLine()
                    : _numberMask.WithBottomLeftVerticalLine();
            }

            // Proceed with vertical detections to be added to the internal state.
            if (topHorizontalLineDetected) { _numberMask = _numberMask.WithTopHorizontalLine(); }
            if (centerHorizontalLineDetected) { _numberMask = _numberMask.WithCenterHorizontalLine(); }
            if (bottomHorizontalLineDetected) { _numberMask = _numberMask.WithBottomHorizontalLine(); }

            return this;
        }

        /// <summary>
        /// Method resets the internal number detection state.
        /// </summary>
        public INumberInterpreter Reset()
        {
            _numberMask = _numberMask.Reset();
            return this;
        }

        /// <summary>
        /// Method tries to interpret a number based on the saved internal detection state. 
        /// </summary>
        /// <returns>(true, <interpreted_number>) in case of interpretetion success and (false, null) otherwise.</returns>
        public (bool sucess, int? output) TryInterpret()
        {
            foreach (var item in _interpretationTemplates)
            {
                if (item.Value.Any(x => x.CheckForMatchWith(_numberMask)))
                {
                    return (true, item.Key);
                }
            }

            return (false, null);
        }


        private bool AnyHorizontalLinesPresentInMask() => _numberMask.BottomHorizontalLine || _numberMask.TopHorizontalLine || _numberMask.CenterHorizontalLine;

        // Method determines a central position index for the given length. 
        // Important while interpreting the top and bottom lines of the number.
        private static int GetCentralPositionIndex(int length)
        {
            if (length <= 0)
            {
                return 0;
            }

            // For odd length returns the middle position.
            // For even length returns the first position of two center elements (-1 because of zero-based indexing)
            return length % 2 == 0
                ? length / 2 - 1
                : length / 2;
        }
    }
}
