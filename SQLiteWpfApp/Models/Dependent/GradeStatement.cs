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

        public Student Student { get; set; }

        public int DisciplineID { get; set; }

        public Discipline Discipline { get; set; }

        public int TeacherID { get; set; }

        public Teacher Teacher { get; set; }

        public string GradeName { get; set; }

        public Grade Grade { get; set; }

        public string PassingDate { get; set; }
    }
}