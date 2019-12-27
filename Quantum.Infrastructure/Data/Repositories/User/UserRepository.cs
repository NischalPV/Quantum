using Microsoft.AspNetCore.Identity;
using Quantum.Core.Entities;
using Quantum.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Quantum.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        protected readonly IdentityDbContext _dbContext;
        private readonly IAppLogger<UserRepository> _logger;

        public UserRepository(IdentityDbContext dbContext, IAppLogger<UserRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<ApplicationUser> GetUserById(string Id)
        {
            return await _dbContext.Users.FindAsync(Id);
        }

        public async Task<string> GetUserNameById(string Id)
        {
            ApplicationUser user = await GetUserById(Id);
            return user.FullName;
        }
    }
}
