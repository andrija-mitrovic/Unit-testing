using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Api.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string FullName { get; set; } = default!;
    }
}
