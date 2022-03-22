using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Users.Api.Contracts
{
    public class UserResponse
    {
        public Guid Id { get; init; }

        public string FullName { get; init; } = default!;
    }
}
