using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Users.Api.Contracts
{
    public class CreateUserRequest
    {
        public string FullName { get; init; } = default!;
    }
}
