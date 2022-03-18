using Demo.Api.Models;
using Demo.Api.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Api.Tests.Unit
{
    public class FakeUserRepository : IUserRepository
    {
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await Task.FromResult(Enumerable.Empty<User>());
        }
    }
}
