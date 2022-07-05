using System;

namespace SRS.Domain.Entities
{
    public class CathedraDefenses : BaseEntity
    {
        public string SurnameAndInitials { get; set; }

        public string ScientificHead { get; set; }

        public int YearOfEnd { get; set; }

        public DateTime DateOfInning { get; set; }

        public DateTime DateOfDefense { get; set; }

        public string ThemeOfWork { get; set; }
    }
}