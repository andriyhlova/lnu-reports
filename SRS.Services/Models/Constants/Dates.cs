using System;

namespace SRS.Services.Models.Constants
{
    public static class Dates
    {
        public const int MinYear = 1900;
        public const string DatePattern = "yyyy-MM-dd";
        public const string UaDatePattern = "dd.MM.yyyy";
        public static readonly DateTime MinDate = new DateTime(2010, 01, 01);
        public static readonly DateTime MaxDate = new DateTime(2030, 01, 01);
    }
}
