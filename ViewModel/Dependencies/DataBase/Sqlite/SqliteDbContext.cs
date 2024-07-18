using Microsoft.EntityFrameworkCore;

using System.Data;
using System.Data.SQLite;

using ViewModel.Interfaces.DataBase;
using System.Collections.Generic;
using System;

namespace ViewModel.Dependencies.DataBase.Sqlite
{
    /// <summary>
    /// Класс контекста базы данных SQLite с строкой подключения и представлениями таблиц, методами
    /// создания представления таблицы из базы данных, сохранения изменения в таблице и выполнения
    /// команды. Реализует <see cref="IDbContext"/>.
    /// </summary>
    public class SqliteDbContext : BaseDbContext, IDbContext
    {
        /// <summary>
        /// Создаёт экземпляр класса <see cref="SqliteDbContext"/>.
        /// </summary>
        /// <param name="connectionString">Строка подключения.</param>
        public SqliteDbContext(string connectionString) : base(connectionString) { }

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
            optionsBuilder.UseSqlite(_connectionString).EnableDetailedErrors();
        }

        /// <summary>
        /// Выполняет команду.
        /// </summary>
        /// <param name="commandString">Строка команды.</param>
        /// <returns>Результат выполнения команды.</returns>
        public override DataTable ExecuteCommand(string commandString,
            Dictionary<string, object>? parameters = null)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(commandString, connection))
                {
                    if (parameters != null)
                    {
                        foreach (var key in parameters.Keys)
                        {
                            command.Parameters.AddWithValue(key, parameters[key] ?? DBNull.Value);
                        }
                    }
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