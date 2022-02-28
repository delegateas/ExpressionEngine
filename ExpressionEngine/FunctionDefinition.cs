namespace ExpressionEngine
{
    /// <summary>
    /// A Function Definition is a map to a collected set of functions.
    /// - currentCellValue() -> formvalue(logicalName())[idx()][attributeLogicalName()]
    /// </summary>
    public class FunctionDefinition : IFunctionDefinition
    {
        private readonly string _from;
        private readonly string _to;

        /// <summary>
        /// Construction a Function Definition
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public FunctionDefinition(string from, string to)
        {
            _from = @from;
            _to = to;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string From()
        {
            return _from;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string To()
        {
            return _to;
        }
    }
}