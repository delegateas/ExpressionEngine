namespace ExpressionEngine
{
    /// <summary>
    /// A Function Definition is a map to a collected set of functions.
    /// - currentCellValue() -> formvalue(logicalName())[idx()][attributeLogicalName()]
    /// </summary>
    public class FunctionDefinition : IFunctionDefinition
    {
        /// <summary>
        /// The from 'function name' which is replaced
        /// </summary>
        public string From { get; set; }
        /// <summary>
        /// The replacement
        /// </summary>
        public string To { get; set; }
    }
}