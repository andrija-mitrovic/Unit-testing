using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.Api.Models;

namespace Users.Api.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();

        Task<User?> GetByIdAsync(Guid id);

        Task<bool> CreateAsync(User user);

        Task<bool> DeleteByIdAsync(Guid id);
    }

}
