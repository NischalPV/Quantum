using Quantum.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Quantum.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetUserById(string Id);
        Task<string> GetUserNameById(string Id);
    }
}
