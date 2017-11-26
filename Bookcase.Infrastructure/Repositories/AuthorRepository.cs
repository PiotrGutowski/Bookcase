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
    public class AuthorRepository : IAuthorRepository
    {
        private readonly BookcaseContext _db;

        public AuthorRepository(BookcaseContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Author>> GetAllAuthorAsync()
        {

            var author = _db.Author.AsNoTracking();
            return await Task.FromResult(author);
        }

        public async Task<IEnumerable<Author>> BrowseAsync(string name = "")
        {
            var author = _db.Author.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(name))
            {
                author = author.Where(x => x.LastName.ToLowerInvariant()
                    .Contains(name.ToLowerInvariant()));
            }
            return await Task.FromResult(author);

        }

        public async Task<Author> GetAuthorByIdAsync(Guid id)
        {

            return await Task.FromResult(_db.Author.SingleOrDefault(x => x.AuthorId == id));

        }

        public async Task<Author> GetAuthorByNameAsync(string name)
        {

            return await Task.FromResult(_db.Author.SingleOrDefault(x =>
                            x.LastName == name));
        }

        public async Task AddAsync(Author author)
        {

            _db.Author.Add(author);
            await _db.SaveChangesAsync();

        }

        public async Task EditAsync(Author author)
        {

            _db.Entry(author).State = EntityState.Modified;
            await _db.SaveChangesAsync();

        }

        public async Task DeleteAsync(Author author)
        {

            _db.Author.Remove(author);
            await _db.SaveChangesAsync();

        }
    }
}
