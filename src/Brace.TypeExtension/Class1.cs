namespace Brace.TypeExtension
{
    public static class ObjectExtensions
    {
        public static string ToStringUpper(this object value)
        {
            return value.ToString().ToUpper();
        }
    }
}
