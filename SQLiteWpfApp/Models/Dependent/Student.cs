﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.ObjectModel;

using SQLiteWpfApp.Models.Independent;

namespace SQLiteWpfApp.Models.Dependent
{
    [Table("Students")]
    [PrimaryKey(nameof(ID))]
    public class Student
    {
        public int ID { get; set; }

        [ForeignKey(nameof(ID))]
        public virtual Person Person { get; set; }

        public int RecordBookNumber { get; set; }

        public bool? IsDeductible { get; set; }

        public int GroupFormationYear { get; set; }

        public string GroupNumber { get; set; }

        public virtual Group Group { get; set; }

        public string? ScholarshipName { get; set; }

        public virtual Scholarship? Scholarship { get; set; }

        public virtual ObservableCollection<GradeStatement> GradeStatements { get; set; }

        public virtual ObservableCollection<StudentDisciplineConnection> Connections { get; set; }
    }
}