using System;

namespace SRS.Domain.Entities
{
    public class CoworkersDefenses : BaseEntity
    {
        public string SurnameAndInitials { get; set; }

        public string PositionAndCathedra { get; set; }

        public string Spetiality { get; set; }

        public DateTime DateOfDefense { get; set; }

        public string ThemeOfWork { get; set; }
    }
}