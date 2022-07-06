namespace UserManagement.Extensions
{
    public static class StringExtensions
    {
        public static string GetFriendlyName(this string val)
        {
            return val.ToString().ToLower().Replace('_', ' ');
        }
    }
}