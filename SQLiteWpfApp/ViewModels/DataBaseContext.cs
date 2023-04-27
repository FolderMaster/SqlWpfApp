using Microsoft.EntityFrameworkCore;

using SQLiteWpfApp.Models;

namespace SQLiteWpfApp.ViewModels
{
    public class DataBaseContext : DbContext
    {
        private string _dataBasePath;

        //public DbSet<Teacher> Teachers { get; set; } = null!;

        //public DbSet<Student> Students { get; set; } = null!;

        //public DbSet<Discipline> Disciplines { get; set; } = null!;

        //public DbSet<StudyForm> StudyForms { get; set; } = null!;

        //public DbSet<Grade> Grades { get; set; } = null!;

        //public DbSet<GradeStatement> GradeStatements { get; set; } = null!;

        //public DbSet<Specialty> Specialties { get; set; } = null!;

        //public DbSet<Person> Persons { get; set; } = null!;

        ////public DbSet<GradeMode> GradeModes { get; set; } = null!;
        
        //public DbSet<Group> Groups { get; set; } = null!;

        //public DbSet<StudentDisciplineConnection> StudentDisciplineConnections { get; set; } =
        //    null!;

        //public DbSet<TeacherDisciplineConnection> TeacherDisciplineConnections { get; set; } =
        //    null!;

        public DbSet<Department> Departments { get; set; } = null!;

        public DbSet<Role> Roles { get; set; } = null!;

        public DbSet<Position> Positions { get; set; } = null!;

        public DbSet<Passport> Passports { get; set; } = null!;

        public DbSet<Scholarship> Scholarships { get; set; } = null!;

        public DataBaseContext(string dataBasePath) => _dataBasePath = dataBasePath;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlite($"Data Source={_dataBasePath}");
    }
}