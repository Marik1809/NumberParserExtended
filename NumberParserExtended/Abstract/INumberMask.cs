namespace NumberParserExtended.Abstract
{
    /// <summary>
    /// Interface represents NUmber Mask that is involved in number interpretation.
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
    internal interface INumberMask
    {
        bool TopHorizontalLine { get; }
        bool CenterHorizontalLine { get; }
        bool BottomHorizontalLine { get; }
        bool TopRightVerticalLine { get; }
        bool TopLeftVerticalLine { get; }
        bool BottomRightVerticalLine { get; }
        bool BottomLeftVerticalLine { get; }

        /// <summary>
        /// Returns true if there is any line marken as "true". It means that the maintained internal detection state is present.
        /// </summary>
        bool ContainsState { get; }

        /// <summary>
        /// Reset saved Number Mask state. Marks all lines as "false"
        /// </summary>
        INumberMask Reset();


        // With- methods mark respective lines "true". Are used to handle the detection state and actual number templates.
        INumberMask WithTopHorizontalLine();
        INumberMask WithCenterHorizontalLine();
        INumberMask WithBottomHorizontalLine();
        INumberMask WithTopRightVerticalLine();
        INumberMask WithTopLeftVerticalLine();
        INumberMask WithBottomRightVerticalLine();
        INumberMask WithBottomLeftVerticalLine();

        /// <summary>
        /// Marks all lines as "true".
        /// </summary>
        /// <returns></returns>
        INumberMask WithAllLines();

        /// <summary>
        /// Compare two masks if they match. Used in the actual number determination.
        /// </summary>
        bool CheckForMatchWith(INumberMask otherMask);
    }
}
