﻿using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace SQLiteWpfApp.Models.Dependent
{
    [Table("Groups")]
    [PrimaryKey(nameof(Number), new string[] { nameof(FormationYear) })]
    public class Group
    {
        public string Number { get; set; }

        public int FormationYear { get; set; }

        public string SpecialtyNumber { get; set; }

        public virtual Specialty Specialty { get; set; }

        public virtual ObservableCollection<Student> Students { get; set; }
    }
}