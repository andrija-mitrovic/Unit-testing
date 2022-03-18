using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Api.Options
{
    public class DbConnectionOptions
    {
        public string ConnectionString { get; init; } = default!;
    }
}
