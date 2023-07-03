using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;

using Model.Independent;
using Model.Dependent;
using ViewModel.Interfaces;

namespace ViewModel.Dependencies
{
    public class SQLiteDbContext : DbContext, IDbContext
    {
        public string ConnectionString { get; private set; } = null!;

        public DbSet<Teacher> Teachers { get; set; } = null!;

        public DbSet<Student> Students { get; set; } = null!;

        public DbSet<Discipline> Disciplines { get; set; } = null!;

        public DbSet<StudyForm> StudyForms { get; set; } = null!;

        public DbSet<Grade> Grades { get; set; } = null!;

        public DbSet<GradeStatement> GradeStatements { get; set; } = null!;

        public DbSet<Specialty> Specialties { get; set; } = null!;

        public DbSet<Person> Persons { get; set; } = null!;

        public DbSet<Group> Groups { get; set; } = null!;

        public DbSet<StudentDisciplineConnection> StudentDisciplineConnections { get; set; } =
            null!;

        public DbSet<TeacherDisciplineConnection> TeacherDisciplineConnections { get; set; } =
            null!;

        public DbSet<Department> Departments { get; set; } = null!;

        public DbSet<Role> Roles { get; set; } = null!;

        public DbSet<Position> Positions { get; set; } = null!;

        public DbSet<Passport> Passports { get; set; } = null!;

        public DbSet<GradeMode> GradeModes { get; set; } = null!;

        public DbSet<Scholarship> Scholarships { get; set; } = null!;

        public SQLiteDbContext(string connectionString)
        {
            ConnectionString = connectionString;
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            base.OnModelCreating(modelBuilder);

        protected override void ConfigureConventions(ModelConfigurationBuilder
            configurationBuilder) => base.ConfigureConventions(configurationBuilder);

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseSqlite(ConnectionString);
        }

        public int SaveChanges<TEntity>() where TEntity : class
        {
            var original = ChangeTracker.Entries().Where(x => !typeof(TEntity)
                .IsAssignableFrom(x.Entity.GetType()) && x.State != EntityState.Unchanged)
                .GroupBy(x => x.State).ToList();

            foreach (var entry in ChangeTracker.Entries().Where(x => !typeof(TEntity)
            .IsAssignableFrom(x.Entity.GetType())))
            {
                entry.State = EntityState.Unchanged;
            }

            var rows = base.SaveChanges();
            foreach (var state in original)
            {
                foreach (var entry in state)
                {
                    entry.State = state.Key;
                }
            }

            return rows;
        }
    }
}