namespace Core.Seascape.UI.Extensions
{
    public static class StringExtensions
    {

        /// <summary>
        /// An extension method to check if a string is not null, empty or consists of only whitespace characters.
        /// </summary>
        /// <param name="value">Instance type to extend.</param>
        /// <returns><see langword="true"/> if the string contains a value. Otherwise <see langword="false"/>.</returns>
        public static bool HasValue(this string value)
        {
            if (value == null || value.Length == 0) return false;

            for (int i = 0; i < value.Length; i++)
            {
                if (!char.IsWhiteSpace(value[i]))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
