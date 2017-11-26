using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bookcase.Core.Domain;

namespace Bookcase.Core.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUserAsync();
        Task<User> GetAsync(Guid id);
        Task<User> GetByEmailAsync(string email);
        Task<IEnumerable<User>> GetByNameAsync(string name="");
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);

    }
}
