﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SQLiteWpfApp.Models.Independent
{
    [Table("GradeModes")]
    [PrimaryKey(nameof(Name))]
    public class GradeMode
    {
        public string Name { get; set; } = "";
    }
}