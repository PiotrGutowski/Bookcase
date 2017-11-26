using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bookcase.Infrastructure.Commands.BorrowedBooks;
using Bookcase.Infrastructure.DTO;

namespace Bookcase.Infrastructure.Services
{
    public interface IBorrowedBooksService
    {
        Task<IEnumerable<BorrowedBooksDto>> GetAllBorrowedBookssAsync();
        Task<IEnumerable<BorrowedBooksDto>> BrowseAsync(string name = null);
        Task AddBorrowedBooksAsync(CreateBorrowedBooks createBorrowedBooks);
        Task DeleteBorrowedBooksAsync(Guid id);

    }
}
