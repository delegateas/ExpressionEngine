namespace ExpressionEngine
{
    /// <summary>
    /// Interface to function definitions
    /// </summary>
    public interface IFunctionDefinition
    {
        /// <summary>
        /// The name of the 'function' which is replaced
        /// </summary>
        string From { get; set; }
        
        /// <summary>
        /// What it is replaced with 
        /// </summary>
        string To { get; set; }
    }
}