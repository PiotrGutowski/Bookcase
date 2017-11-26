using Bookcase.ClientRepository.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookcase.ClientRepository.IRepositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBookAsync();
        Task<Book> GetBookByIdAsync(Guid? id);
        Task<IEnumerable<Book>> GetBookByNameAsync(string name);
        Task AddAsync(Book book);
        Task EditAsync(Book book);
        Task DeleteAsync(Guid id);
    }
}
