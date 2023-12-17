using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using System.Linq;
using System.Data;

using Model.Independent;
using Model.Dependent;

using ViewModel.Interfaces.DataBase;

using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace ViewModel.Dependencies.DataBase
{
    public abstract class BaseDbContext : DbContext, IDbContext
    {
        /// <summary>
        /// Строка подключения.
        /// </summary>
        protected string _connectionString;

        /// <summary>
        /// Возвращает и задаёт представление таблицы учителей <seealso cref="Teacher"/> из базы
        /// данных.
        /// </summary>
        public DbSet<Teacher> Teachers { get; set; }

        /// <summary>
        /// Возвращает и задаёт представление таблицы студентов <seealso cref="Student"/> из базы
        /// данных.
        /// </summary>
        public DbSet<Student> Students { get; set; }

        /// <summary>
        /// Возвращает и задаёт представление таблицы дисциплин <seealso cref="Discipline"/> из
        /// базы данных.
        /// </summary>
        public DbSet<Discipline> Disciplines { get; set; }

        /// <summary>
        /// Возвращает и задаёт представление таблицы режимов обучения <seealso cref="StudyForm"/>
        /// из базы данных.
        /// </summary>
        public DbSet<StudyForm> StudyForms { get; set; }

        /// <summary>
        /// Возвращает и задаёт представление таблицы оценок <seealso cref="Grade"/> из базы
        /// данных.
        /// </summary>
        public DbSet<Grade> Grades { get; set; }

        /// <summary>
        /// Возвращает и задаёт представление таблицы ведомостей оценивания
        /// <seealso cref="GradeStatement"/> из базы данных.
        /// </summary>
        public DbSet<GradeStatement> GradeStatements { get; set; }

        /// <summary>
        /// Возвращает и задаёт представление таблицы специальностей <seealso cref="Specialty"/> из
        /// базы данных.
        /// </summary>
        public DbSet<Specialty> Specialties { get; set; }

        /// <summary>
        /// Возвращает и задаёт представление таблицы персон <seealso cref="Person"/> из базы
        /// данных.
        /// </summary>
        public DbSet<Person> Persons { get; set; }

        /// <summary>
        /// Возвращает и задаёт представление таблицы групп <seealso cref="Group"/> из базы данных.
        /// </summary>
        public DbSet<Group> Groups { get; set; }

        /// <summary>
        /// Возвращает и задаёт представление таблицы связей между дисциплинами и преподавателями
        /// <seealso cref="TeacherDisciplineConnection"/> из базы данных.
        /// </summary>
        public DbSet<TeacherDisciplineConnection> TeacherDisciplineConnections { get; set; }

        /// <summary>
        /// Возвращает и задаёт представление таблицы связей между дисциплинами и студентами
        /// <seealso cref="StudentDisciplineConnection"/> из базы данных.
        /// </summary>
        public DbSet<StudentDisciplineConnection> StudentDisciplineConnections { get; set; }

        /// <summary>
        /// Возвращает и задаёт представление таблицы связей между дисциплинами и преподавателями
        /// <seealso cref="TeacherDisciplineConnection"/> из базы данных.
        /// </summary>
        public DbSet<Department> Departments { get; set; }

        /// <summary>
        /// Возвращает и задаёт представление таблицы ролей <seealso cref="Role"/> из базы данных.
        /// </summary>
        public DbSet<Role> Roles { get; set; }

        /// <summary>
        /// Возвращает и задаёт представление таблицы должностей <seealso cref="Position"/> из базы
        /// данных.
        /// </summary>
        public DbSet<Position> Positions { get; set; }

        /// <summary>
        /// Возвращает и задаёт представление таблицы паспортов <seealso cref="Passport"/> из базы
        /// данных.
        /// </summary>
        public DbSet<Passport> Passports { get; set; }

        /// <summary>
        /// Возвращает и задаёт представление таблицы режимов оценивания
        /// <seealso cref="GradeMode"/> из базы данных.
        /// </summary>
        public DbSet<GradeMode> GradeModes { get; set; }

        /// <summary>
        /// Возвращает и задаёт представление таблицы стипендий <seealso cref="Scholarship"/> из
        /// базы данных.
        /// </summary>
        public DbSet<Scholarship> Scholarships { get; set; }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="SqliteDbContext"/>.
        /// </summary>
        /// <param name="connectionString">Строка подключения.</param>
        public BaseDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Создаёт модель.
        /// </summary>
        /// <param name="modelBuilder">Создатель моделей.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            base.OnModelCreating(modelBuilder);

        /// <summary>
        /// Настраивает соглашения.
        /// </summary>
        /// <param name="configurationBuilder">Создатель конфигурации.</param>
        protected override void ConfigureConventions(ModelConfigurationBuilder
            configurationBuilder) => base.ConfigureConventions(configurationBuilder);

        /// <summary>
        /// Настраивает базу данных.
        /// </summary>
        /// <param name="optionsBuilder">Создатель опций.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => base.OnConfiguring(optionsBuilder);

        /// <summary>
        /// Сохраняет изменения в таблице.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности таблицы.</typeparam>
        /// <returns>Количество сохранённых изменённых строк.</returns>
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

        public void Reload<TEntity>() where TEntity : class
        {
            foreach (var row in Set<TEntity>())
            {
                Update(row);
            }
        }

        public void RejectChanges<TEntity>() where TEntity : class
        {
            
        }

        public ObservableCollection<TEntity> GetDbSetLocal<TEntity>() where TEntity : class
        {
            var dbSet = Set<TEntity>();
            if (!dbSet.Local.Any())
            {
                dbSet.Load();
            }
            return dbSet.Local.ToObservableCollection();
        }

        public bool CanConnect() => Database.CanConnect();

        public abstract DataTable ExecuteCommand(string commandString, Dictionary<string, object>? parameters = null);
    }
}
