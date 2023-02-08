namespace sights.Utility
{
    public static class Utility
    {
        public static string? FirstLetterToUpper(string str)
        {
            if (str == null)
                return null;

            if (str.Length > 1)
                return char.ToUpper(str[0]) + str[1..];

            return str.ToUpper();
        }
    }
}
