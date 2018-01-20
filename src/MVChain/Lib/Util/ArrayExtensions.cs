namespace MVChain.Lib.Util
{
    internal static class ArrayExtensions
    {
        internal static bool IsNullOrEmpty<T>(this T[] array) =>
            array == null || array.Length == 0;
    }
}