using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bookcase.Infrastructure.DTO;

namespace Bookcase.Infrastructure.Services
{
   public interface IBookService
   {
        Task<IEnumerable<BookDto>> GetAllBooksAsync();
        Task<BookDto> GetBookByNameAsync(string name);
        Task<IEnumerable<BookDto>> BrowseAsync(string name = null);
        Task AddBookAsync(Guid bookId, Guid authorId, string title, string ISBN, DateTime published, bool isAvailable);
        Task EditBookAsync(Guid bookId, Guid authorId, string title, string ISBN, DateTime published, bool isAvailable);
        Task DeleteBookAsync(Guid id);

    }
}
