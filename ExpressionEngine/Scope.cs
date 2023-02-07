namespace ExpressionEngine
{
    /// <summary>
    /// Representing the scopes available for dependency injection
    /// </summary>
    public enum Scope
    {
        /// <summary>
        /// Scoped
        /// </summary>
        Scoped,

        /// <summary>
        /// Transient
        /// </summary>
        Transient,

        /// <summary>
        /// Singleton
        /// </summary>
        Singleton
    }
}