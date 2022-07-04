using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SRS.Domain.Entities
{
    public class OtherDefenses
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string SurnameAndInitials { get; set; }

        public string ScientificHead { get; set; }

        public string Spetiality { get; set; }

        public DateTime DateOfDefense { get; set; }

        public string ThemeOfWork { get; set; }
    }
}