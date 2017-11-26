using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bookcase.Core.Domain;

namespace Bookcase.Core.Repositories
{
    public interface IBorrowedBooksRepository
    {
        Task<IEnumerable<BorrowedBooks>> GetAllBorrowedBooksAsync();
        Task<BorrowedBooks> GetBorrowedBooksByIdAsync(Guid id);
        Task<IEnumerable<BorrowedBooks>> BrowseAsync(string name = "");
        Task AddAsync(BorrowedBooks borrowedBooks);
        Task DeleteAsync(BorrowedBooks borrowedBooks);

    }
}
