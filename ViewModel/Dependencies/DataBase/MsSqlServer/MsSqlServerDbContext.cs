﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

using System.Data;
using System.Collections.Generic;
using System;

using Model.Dependent;
using Model.Independent;

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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().ToTable((t) => t.HasTrigger("GenerateNameOnUpdate"));
            modelBuilder.Entity<Passport>().ToTable((t) => t.HasTrigger("GenerateNameOnInsert"));
            base.OnModelCreating(modelBuilder);
        }

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
            IDictionary<string, object>? parameters = null)
        {
            Database.OpenConnection();
            using (var command = Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = commandString;
                if (parameters != null)
                {
                    foreach (var pair in parameters)
                    {
                        command.Parameters.Add(new SqlParameter(pair.Key,
                            pair.Value ?? DBNull.Value));
                    }
                }
                using (var reader = command.ExecuteReader())
                {
                    var dataTable = new DataTable();
                    dataTable.Load(reader);
                    return dataTable;
                }
            }
        }
    }
}
