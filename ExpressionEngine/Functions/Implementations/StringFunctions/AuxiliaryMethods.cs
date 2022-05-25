namespace ExpressionEngine.Functions.Implementations.StringFunctions
{
    public static class AuxiliaryMethods
    {
        public static string VcIsString(ValueContainer vc)
        {
            return vc.Type() == ValueType.String
                ? vc.GetValue<string>()
                : throw new ExpressionEngineException("ValueContainer must be of type string.");
        }
    }
}
