using Demo.Api.Models;
using Demo.Api.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Api.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _logger = new Logger<UserService>(new LoggerFactory());
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            _logger.LogInformation("Retrieving all users");
            var stopwatch = Stopwatch.StartNew();

            try
            {
                var users = await _userRepository.GetAllAsync();
                return users;
            }
            finally
            {
                stopwatch.Stop();
                _logger.LogInformation("All users retrieved in {0}ms", stopwatch.ElapsedMilliseconds);
            }
        }
    }
}
