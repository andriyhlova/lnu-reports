using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SRS.Domain.Entities
{
    public class CathedraDefenses
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string SurnameAndInitials { get; set; }

        public string ScientificHead { get; set; }

        public int YearOfEnd { get; set; }

        public DateTime DateOfInning { get; set; }

        public DateTime DateOfDefense { get; set; }

        public string ThemeOfWork { get; set; }
    }
}