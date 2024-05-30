using System;
using System.ComponentModel.DataAnnotations;

namespace SRS.Services.Extensions
{
    public class YearRangeAttribute : RangeAttribute
    {
        public YearRangeAttribute(int minimumYear)
            : base(minimumYear, DateTime.Now.Year)
        {
        }
    }
}
