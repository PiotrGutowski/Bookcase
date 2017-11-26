using Bookcase.ClientRepository.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookcase.ClientRepository.IRepositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUserAsync();
        Task<IEnumerable<User>> GetUserByNameAsync(string name);
        Task<User> GetUserByIdAsync(Guid? id);
        Task AddUserAsync(User user);
        Task EditAsync(User User);
        Task DeleteAsync(Guid id);
    }
}
