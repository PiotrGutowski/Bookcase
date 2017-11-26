using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bookcase.Infrastructure.DTO;

namespace Bookcase.Infrastructure.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUserAsync();
        Task<UserDto> GetUserByIdAsync(Guid userId);
        Task<IEnumerable<UserDto>> GetUserByNameAsync(string name = null);
        Task AddUserAsync(Guid userId, string email,string name);
        Task EditUserAsync(Guid userId, string email, string name);
        Task DeleteUserAsync(Guid userId);
    }
}
