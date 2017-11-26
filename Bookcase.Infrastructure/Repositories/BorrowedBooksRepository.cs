using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Bookcase.Core.Domain;
using Bookcase.Core.Repositories;
using Bookcase.Infrastructure.BookcaseDbContext;
using System.Data;

namespace Bookcase.Infrastructure.Repositories
{
    public class BorrowedBooksRepository : IBorrowedBooksRepository
    {

        private readonly BookcaseContext _db;

        public BorrowedBooksRepository(BookcaseContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<BorrowedBooks>> GetAllBorrowedBooksAsync()
        {

            var borrowedBooks = _db.BorrowedBooks.Include(x => x.User).Include(x => x.Book);
            return await Task.FromResult(borrowedBooks);

        }

        public async Task<IEnumerable<BorrowedBooks>> BrowseAsync(string name = "")
        {
            var borrowedBooks = _db.BorrowedBooks.Include(x => x.User).Include(x => x.Book).AsEnumerable();
            {
                borrowedBooks = borrowedBooks.Where(x => x.Book.Title.ToLowerInvariant()
                    .Contains(name.ToLowerInvariant()));
            }
            return await Task.FromResult(borrowedBooks);

        }

        public async Task<BorrowedBooks> GetBorrowedBooksByIdAsync(Guid id)
        {
            var borrowedBooks = _db.BorrowedBooks.SingleOrDefault(x => x.BorrowedBooksId == id);
            return await Task.FromResult(borrowedBooks);

        }

        public async Task AddAsync(BorrowedBooks borrowedBooks)
        {

            _db.BorrowedBooks.Add(borrowedBooks);
            var book = await _db.Book.FindAsync(borrowedBooks.BookId);
            book.SetIsAvailable(false);
            _db.Entry(book).State = EntityState.Modified;
            await _db.SaveChangesAsync();

        }

        public async Task DeleteAsync(BorrowedBooks borrowedBooks)
        {
            _db.BorrowedBooks.Remove(borrowedBooks);
            var book = await _db.Book.FindAsync(borrowedBooks.BookId);
            book.SetIsAvailable(true);
            _db.Entry(book).State = EntityState.Modified;
            await _db.SaveChangesAsync();

        }
    }
}
