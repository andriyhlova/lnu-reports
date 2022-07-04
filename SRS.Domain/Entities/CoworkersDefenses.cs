using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SRS.Domain.Entities
{
    public class CoworkersDefenses
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string SurnameAndInitials { get; set; }

        public string PositionAndCathedra { get; set; }

        public string Spetiality { get; set; }

        public DateTime DateOfDefense { get; set; }

        public string ThemeOfWork { get; set; }
    }
}