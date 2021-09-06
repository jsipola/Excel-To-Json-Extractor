namespace main
{
    static class StringExtensions
    {
        public static string TruncateLength(this string str, int length)
        {
            if (str.Length > length)
            {
                return str.Substring(0, length);
            }
            else
            {
                return str;
            }
        }
    }
}