using System;

namespace SRS.Domain.Entities
{
    public class OtherDefenses : BaseEntity
    {
        public string SurnameAndInitials { get; set; }

        public string ScientificHead { get; set; }

        public string Spetiality { get; set; }

        public DateTime DateOfDefense { get; set; }

        public string ThemeOfWork { get; set; }
    }
}