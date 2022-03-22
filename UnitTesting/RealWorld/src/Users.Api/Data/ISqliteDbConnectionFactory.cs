using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Users.Api.Data
{
    public interface ISqliteDbConnectionFactory
    {
        Task<IDbConnection> CreateDbConnectionAsync();
    }
}
