using Demo.Api.Options;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Api.Data
{
    public class SqliteDbConnectionFactory
    {
        private readonly DbConnectionOptions _connectionOptions;

        public SqliteDbConnectionFactory()
        {
            _connectionOptions = new DbConnectionOptions
            {
                ConnectionString = "Data Source=./database.db"
            };
        }

        public async Task<IDbConnection> CreateDbConnectionAsync()
        {
            var connection = new SqliteConnection(_connectionOptions.ConnectionString);
            await connection.OpenAsync();
            return connection;
        }
    }
}
