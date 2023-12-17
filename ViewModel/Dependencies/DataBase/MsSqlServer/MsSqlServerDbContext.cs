using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

using System.Data;

using System.Collections.Generic;

namespace ViewModel.Dependencies.DataBase.MsSqlServer
{
    public class MsSqlServerDbContext : BaseDbContext
    {
        /// <summary>
        /// Создаёт экземпляр класса <see cref="SqliteDbContext"/>.
        /// </summary>
        /// <param name="connectionString">Строка подключения.</param>
        public MsSqlServerDbContext(string connectionString) : base(connectionString) { }

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
            optionsBuilder.UseSqlServer(_connectionString);
        }

        /// <summary>
        /// Выполняет команду.
        /// </summary>
        /// <param name="commandString">Строка команды.</param>
        /// <returns>Результат выполнения команды.</returns>
        public override DataTable ExecuteCommand(string commandString,
            Dictionary<string, object>? parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(commandString, connection))
                {
                    if(parameters != null)
                    {
                        foreach (var key in parameters.Keys)
                        {
                            command.Parameters.AddWithValue(key, parameters[key]);
                        }
                    }
                    using (SqlDataReader reader = command.ExecuteReader())
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
