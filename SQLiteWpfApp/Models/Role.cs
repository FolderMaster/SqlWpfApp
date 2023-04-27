using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace SQLiteWpfApp.Models
{
    [Table("Roles")]
    [PrimaryKey(nameof(Name))]
    public class Role
    {
        public string Name { get; set; }
    }
}