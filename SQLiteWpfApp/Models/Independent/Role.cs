using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SQLiteWpfApp.Models.Independent
{
    [Table("Roles")]
    [PrimaryKey(nameof(Name))]
    public class Role
    {
        public string Name { get; set; } = "";
    }
}