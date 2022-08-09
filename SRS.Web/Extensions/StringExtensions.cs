namespace SRS.Web.Extensions
{
    public static class StringExtensions
    {
        public static string GetFriendlyName(this string val)
        {
            return val.ToLower().Replace('_', ' ');
        }
    }
}