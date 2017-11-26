using Bookcase.ClientRepository.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookcase.ClientRepository.IRepositories
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAllAuthorsAsync();
        Task<Author> GetAuthorByIdAsync(Guid? id);
        Task<IEnumerable<Author>> GetAuthorByNameAsync(string name);
        Task AddAsync(Author author);
        Task EditAsync(Author author);
        Task DeleteAsync(Guid id);
    }
}
