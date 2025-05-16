namespace NumberParserExtended.Abstract
{
    /// <summary>
    /// Iterface representing the Number Interpreter, that "consumes" raw symbolic data column by column and preserves number detection state. 
    /// </summary>
    internal interface INumberInterpreter
    {
        /// <summary>
        /// Returns true if there is current detection state saved in the current interpreter.
        /// </summary>
        bool ContainsState { get; }

        /// <summary>
        /// Method takes a new raw symbolic column as the input, performs a part of number interpretation and updates the internal state of the current Interpreter.
        /// </summary>
        /// <param name="column">Raw symbolic input column for interpretetation.</param>
        INumberInterpreter ProcessLineColumn(char[] column);

        /// <summary>
        /// Method resets the internal number detection state.
        /// </summary>
        INumberInterpreter Reset();

        /// <summary>
        /// Method tries to interpret a number based on the saved internal detection state. 
        /// </summary>
        /// <returns>(true, <interpreted_number>) in case of interpretetion success and (false, null) otherwise.</returns>
        (bool sucess, int? output) TryInterpret();
    }
}
