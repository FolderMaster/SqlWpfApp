using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SQLiteWpfApp.Models.Dependent
{
    [Table("GradeStatements")]
    [PrimaryKey(nameof(ID))]
    public class GradeStatement
    {
        public int ID { get; set; }

        public int StudentID { get; set; }

        public virtual Student Student { get; set; }

        public int DisciplineID { get; set; }

        public virtual Discipline Discipline { get; set; }

        public int TeacherID { get; set; }

        public virtual Teacher Teacher { get; set; }

        public string GradeName { get; set; }

        public virtual Grade Grade { get; set; }

        public DateTime PassingDate { get; set; }
    }
}