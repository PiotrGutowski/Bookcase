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
    public class BookRepository : IBookRepository
    {
        private readonly BookcaseContext _db;

        public BookRepository(BookcaseContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Book>> GetAllBookAsync()
        {
            var book = _db.Book.Include(o => o.Author);
            return await Task.FromResult(book);

        }

        public async Task<IEnumerable<Book>> BrowseAsync(string name = "")
        {

            var book = _db.Book.Include(o => o.Author).AsEnumerable();
            if (!string.IsNullOrWhiteSpace(name))
            {
                book = book.Where(x => x.Title.ToLowerInvariant()
                    .Contains(name.ToLowerInvariant()));
            }
            return await Task.FromResult(book);

        }

        public async Task<Book> GetBookByIdAsync(Guid id)
        {
            var book = _db.Book.SingleOrDefault(x => x.BookId == id);
            return await Task.FromResult(book);

        }

        public async Task<Book> GetBookByNameAsync(string name)
        {

            return await Task.FromResult(_db.Book.SingleOrDefault(x =>
                            x.Title == name));

        }

        public async Task AddAsync(Book book)
        {
            _db.Book.Add(book);
            await _db.SaveChangesAsync();

        }

        public async Task EditAsync(Book book)
        {

            _db.Entry(book).State = EntityState.Modified;
            await _db.SaveChangesAsync();

        }

        public async Task DeleteAsync(Book book)
        {

            _db.Book.Remove(book);
            await _db.SaveChangesAsync();

        }
    }
}
