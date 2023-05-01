using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SQLiteWpfApp.Models.Independent
{
    [Table("Positions")]
    [PrimaryKey(nameof(Name))]
    public class Position
    {
        public string Name { get; set; } = "";
    }
}