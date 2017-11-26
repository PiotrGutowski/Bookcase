using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bookcase.Core.Domain;

namespace Bookcase.Core.Repositories
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAllAuthorAsync();
        Task<Author> GetAuthorByIdAsync(Guid id);
        Task<Author> GetAuthorByNameAsync(string name);
        Task<IEnumerable<Author>> BrowseAsync(string name = "");
        Task AddAsync(Author author);
        Task EditAsync(Author author);
        Task DeleteAsync(Author author);

    }
}
