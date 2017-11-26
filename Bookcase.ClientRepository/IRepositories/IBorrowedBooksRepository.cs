using Bookcase.ClientRepository.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookcase.ClientRepository.IRepositories
{
    public interface IBorrowedBooksRepository
    {
        Task<IEnumerable<BorrowedBooks>> GetAllBorrowedBooksAsync();
        Task<BorrowedBooks> GetBorrowedBooksByIdAsync(Guid? id);
        Task<IEnumerable<BorrowedBooks>> GetBorrowedBooksByNameAsync(string name);
        Task AddAsync(BorrowedBooks borrowedBooks);
        Task DeleteAsync(Guid id);

    }
}
