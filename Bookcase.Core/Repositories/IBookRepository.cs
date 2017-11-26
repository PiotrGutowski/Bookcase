using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bookcase.Core.Domain;

namespace Bookcase.Core.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBookAsync();
        Task<Book> GetBookByIdAsync(Guid id);
        Task<Book> GetBookByNameAsync(string name);
        Task<IEnumerable<Book>> BrowseAsync(string name = "");
        Task AddAsync(Book book);
        Task EditAsync(Book book);
        Task DeleteAsync(Book book);


    }
}
