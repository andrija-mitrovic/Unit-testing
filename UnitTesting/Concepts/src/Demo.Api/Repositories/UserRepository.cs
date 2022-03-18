using Dapper;
using Demo.Api.Data;
using Demo.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SqliteDbConnectionFactory _connectionFactory;

        public UserRepository()
        {
            _connectionFactory = new SqliteDbConnectionFactory();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            using var connection = await _connectionFactory.CreateDbConnectionAsync();
            return await connection.QueryAsync<User>("select * from Users");
        }
    }
}
