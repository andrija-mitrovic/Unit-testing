using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Users.Api.Models
{
    public class User
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        public string FullName { get; set; } = default!;
    }
}
