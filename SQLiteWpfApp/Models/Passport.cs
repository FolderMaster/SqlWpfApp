using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SQLiteWpfApp.Models
{
    [Table("Passports")]
    [PrimaryKey(nameof(SerialNumber))]
    public class Passport
    {
        public int SerialNumber { get; set; }

        public string Name { get; set; }

        public string PermanentResidenceAddress { get; set; }

        public Guid Scan { get; set; }
    }
}