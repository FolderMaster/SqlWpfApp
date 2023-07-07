using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using System.Linq;
using System.Data;
using System.Data.SQLite;

using Model.Independent;
using Model.Dependent;
using ViewModel.Interfaces.DbContext;

namespace ViewModel.Dependencies
{
    /// <summary>
    /// Класс контекста базы данных SQLite с строкой подключения и представлениями таблиц, методами
    /// создания представления таблицы из базы данных, сохранения изменения в таблице и выполнения
    /// команды. Реализует <see cref="IDbContext"/>.
    /// </summary>
    public class SQLiteDbContext : DbContext, IDbContext
    {
        /// <summary>
        /// Строка подключения.
        /// </summary>
        private string _connectionString;

        /// <summary>
        /// Возвращает и задаёт представление таблицы учителей <seealso cref="Teacher"/> из базы
        /// данных.
        /// </summary>
        public DbSet<Teacher> Teachers { get; set; } = null!;

        /// <summary>
        /// Возвращает и задаёт представление таблицы студентов <seealso cref="Student"/> из базы
        /// данных.
        /// </summary>
        public DbSet<Student> Students { get; set; } = null!;

        /// <summary>
        /// Возвращает и задаёт представление таблицы дисциплин <seealso cref="Discipline"/> из
        /// базы данных.
        /// </summary>
        public DbSet<Discipline> Disciplines { get; set; } = null!;

        /// <summary>
        /// Возвращает и задаёт представление таблицы режимов обучения <seealso cref="StudyForm"/>
        /// из базы данных.
        /// </summary>
        public DbSet<StudyForm> StudyForms { get; set; } = null!;

        /// <summary>
        /// Возвращает и задаёт представление таблицы оценок <seealso cref="Grade"/> из базы
        /// данных.
        /// </summary>
        public DbSet<Grade> Grades { get; set; } = null!;

        /// <summary>
        /// Возвращает и задаёт представление таблицы ведомостей оценивания
        /// <seealso cref="GradeStatement"/> из базы данных.
        /// </summary>
        public DbSet<GradeStatement> GradeStatements { get; set; } = null!;

        /// <summary>
        /// Возвращает и задаёт представление таблицы специальностей <seealso cref="Specialty"/> из
        /// базы данных.
        /// </summary>
        public DbSet<Specialty> Specialties { get; set; } = null!;

        /// <summary>
        /// Возвращает и задаёт представление таблицы персон <seealso cref="Person"/> из базы
        /// данных.
        /// </summary>
        public DbSet<Person> Persons { get; set; } = null!;

        /// <summary>
        /// Возвращает и задаёт представление таблицы групп <seealso cref="Group"/> из базы данных.
        /// </summary>
        public DbSet<Group> Groups { get; set; } = null!;

        /// <summary>
        /// Возвращает и задаёт представление таблицы связей между дисциплинами и студентами
        /// <seealso cref="StudentDisciplineConnection"/> из базы данных.
        /// </summary>
        public DbSet<StudentDisciplineConnection> StudentDisciplineConnections { get; set; } =
            null!;

        /// <summary>
        /// Возвращает и задаёт представление таблицы связей между дисциплинами и преподавателями
        /// <seealso cref="TeacherDisciplineConnection"/> из базы данных.
        /// </summary>
        public DbSet<TeacherDisciplineConnection> TeacherDisciplineConnections { get; set; } =
            null!;

        /// <summary>
        /// Возвращает и задаёт представление таблицы связей между дисциплинами и преподавателями
        /// <seealso cref="TeacherDisciplineConnection"/> из базы данных.
        /// </summary>
        public DbSet<Department> Departments { get; set; } = null!;

        /// <summary>
        /// Возвращает и задаёт представление таблицы ролей <seealso cref="Role"/> из базы данных.
        /// </summary>
        public DbSet<Role> Roles { get; set; } = null!;

        /// <summary>
        /// Возвращает и задаёт представление таблицы должностей <seealso cref="Position"/> из базы
        /// данных.
        /// </summary>
        public DbSet<Position> Positions { get; set; } = null!;

        /// <summary>
        /// Возвращает и задаёт представление таблицы паспортов <seealso cref="Passport"/> из базы
        /// данных.
        /// </summary>
        public DbSet<Passport> Passports { get; set; } = null!;

        /// <summary>
        /// Возвращает и задаёт представление таблицы режимов оценивания
        /// <seealso cref="GradeMode"/> из базы данных.
        /// </summary>
        public DbSet<GradeMode> GradeModes { get; set; } = null!;

        /// <summary>
        /// Возвращает и задаёт представление таблицы стипендий <seealso cref="Scholarship"/> из
        /// базы данных.
        /// </summary>
        public DbSet<Scholarship> Scholarships { get; set; } = null!;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="SQLiteDbContext"/>.
        /// </summary>
        /// <param name="connectionString">Строка подключения.</param>
        public SQLiteDbContext(string connectionString)
        {
            _connectionString = connectionString;
            Database.EnsureCreated();
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
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseSqlite(_connectionString).EnableDetailedErrors();
        }

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

        /// <summary>
        /// Выполняет команду.
        /// </summary>
        /// <param name="commandString">Строка команды.</param>
        /// <returns>Результат выполнения команды.</returns>
        public DataTable ExecuteCommand(string commandString)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(commandString, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        var dataTable = new DataTable();
                        dataTable.Load(reader);
                        return dataTable;
                    }
                }
            }
        }
    }
}