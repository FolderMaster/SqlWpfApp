using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SQLiteWpfApp.Models.Independent
{
    [Table("Passports")]
    [PrimaryKey(nameof(SerialNumber))]
    public class Passport
    {
        public int SerialNumber { get; set; }

        public string Name { get; set; } = "";

        public string PermanentResidenceAddress { get; set; } = "";

        public bool Sex { get; set; }

        public byte[] Scan { get; set; } = null;
    }
}