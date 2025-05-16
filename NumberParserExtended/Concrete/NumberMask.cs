using NumberParserExtended.Abstract;

namespace NumberParserExtended.Concrete
{

    /// <summary>
    /// Class represents NUmber Mask that is involved in number interpretation.
    /// NumberMask is used to store the internal number detection state, to represent the number templates used for detection amd interpretation.
    /// 
    /// The general idea is the "Calculator-based" number presentation, that consists of 3 possible horizontal lines and 4 possible vertical lines.
    /// All lines are represented as boolean flags, that can be set to true if the specific line is present in a number template of in a detection state.
    /// 
    /// 
    ///        Top left vertical |    | Top right vertical
    ///                          V    V
    /// 
    /// Top horizontal      ->    ----
    ///                          |    |
    /// Middle horizontal   ->    ----
    ///                          |    |
    /// Bottom Horizontal   ->    ----
    /// 
    ///                          ^    ^
    ///     Bottom left vertical |    | Bottom right vertical 
    ///     
    /// </summary>
    internal class NumberMask : INumberMask
    {
        public bool TopHorizontalLine { get; private set; }
        public bool CenterHorizontalLine { get; private set; }
        public bool BottomHorizontalLine { get; private set; }
        public bool TopRightVerticalLine { get; private set; }
        public bool TopLeftVerticalLine { get; private set; }
        public bool BottomRightVerticalLine { get; private set; }
        public bool BottomLeftVerticalLine { get; private set; }


        /// <summary>
        /// Returns true if there is any line marken as "true". It means that the maintained internal detection state is present.
        /// </summary>
        public bool ContainsState => TopHorizontalLine
            || CenterHorizontalLine
            || BottomHorizontalLine
            || TopRightVerticalLine
            || BottomRightVerticalLine
            || TopLeftVerticalLine
            || BottomLeftVerticalLine;

        /// <summary>
        /// Compare two masks if they match. Used in the actual number determination.
        /// </summary>
        public bool CheckForMatchWith(INumberMask otherMask)
        => otherMask != null
                    && TopHorizontalLine == otherMask.TopHorizontalLine
                    && CenterHorizontalLine == otherMask.CenterHorizontalLine
                    && BottomHorizontalLine == otherMask.BottomHorizontalLine
                    && TopRightVerticalLine == otherMask.TopRightVerticalLine
                    && TopLeftVerticalLine == otherMask.TopLeftVerticalLine
                    && BottomRightVerticalLine == otherMask.BottomRightVerticalLine
                    && BottomLeftVerticalLine == otherMask.BottomLeftVerticalLine;

        /// <summary>
        /// Reset saved Number Mask state. Marks all lines as "false"
        /// </summary>
        public INumberMask Reset()
        {
            TopHorizontalLine = CenterHorizontalLine = BottomHorizontalLine
                    = TopRightVerticalLine = TopLeftVerticalLine = BottomRightVerticalLine
                    = BottomLeftVerticalLine = false;

            return this;
        }

        /// <summary>
        /// Marks all lines as "true".
        /// </summary>
        public INumberMask WithAllLines()
        {
            TopHorizontalLine = CenterHorizontalLine = BottomHorizontalLine
                   = TopRightVerticalLine = TopLeftVerticalLine = BottomRightVerticalLine
                   = BottomLeftVerticalLine = true;

            return this;
        }

        // With- methods mark respective lines "true". Are used to handle the detection state and actual number templates.

        public INumberMask WithBottomHorizontalLine()
        {
            BottomHorizontalLine = true;
            return this;
        }

        public INumberMask WithBottomLeftVerticalLine()
        {
            BottomLeftVerticalLine = true;
            return this;
        }

        public INumberMask WithBottomRightVerticalLine()
        {
            BottomRightVerticalLine = true;
            return this;
        }

        public INumberMask WithCenterHorizontalLine()
        {
            CenterHorizontalLine = true;
            return this;
        }

        public INumberMask WithTopHorizontalLine()
        {
            TopHorizontalLine = true;
            return this;
        }

        public INumberMask WithTopLeftVerticalLine()
        {
            TopLeftVerticalLine = true;
            return this;
        }

        public INumberMask WithTopRightVerticalLine()
        {
            TopRightVerticalLine = true;
            return this;
        }
    }
}
