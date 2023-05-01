using Microsoft.EntityFrameworkCore;
using SQLiteWpfApp.Models.Dependent;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SQLiteWpfApp.Models.Independent
{
    [Table("Departments")]
    [PrimaryKey(nameof(Name))]
    public class Department
    {
        public string Name { get; set; } = "";

        public string Symbol { get; set; } = "";
    }
}