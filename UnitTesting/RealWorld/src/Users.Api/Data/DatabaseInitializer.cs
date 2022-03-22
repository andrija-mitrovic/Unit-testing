﻿using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.Api.Models;

namespace Users.Api.Data
{
    public class DatabaseInitializer
    {
        private readonly ISqliteDbConnectionFactory _connectionFactory;

        public DatabaseInitializer(ISqliteDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task InitializeAsync()
        {
            SqlMapper.AddTypeHandler(new SqLiteGuidTypeHandler());
            SqlMapper.RemoveTypeMap(typeof(Guid));
            SqlMapper.RemoveTypeMap(typeof(Guid?));

            using var connection = await _connectionFactory.CreateDbConnectionAsync();
            await connection.ExecuteAsync("CREATE TABLE IF NOT EXISTS Users (Id TEXT PRIMARY KEY, FullName TEXT NOT NULL)");
            var result = await connection.QuerySingleOrDefaultAsync<User>("SELECT * FROM Users where FullName = @FullName",
                                    new { FullName = "Andrija Mitrovic" });

            if (result is null)
            {
                await connection.ExecuteAsync("INSERT INTO Users (Id, FullName) VALUES (@Id, @FullName)", 
                                    new { Id = Guid.NewGuid().ToString(), FullName = "Andrija Mitrovic" });
            }
        }
    }
}
