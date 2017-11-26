using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bookcase.Infrastructure.DTO;

namespace Bookcase.Infrastructure.Services
{
   public interface IAuthorService
    {
        Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync();
        Task<AuthorDto> GetAuthorByNameAsync(string name);
        Task<IEnumerable<AuthorDto>> BrowseAsync(string name = null);
        Task AddAuthorAsync(Guid authorId, string firstName, string lastName);
        Task EditAuthorAsync(Guid authorId, string firstName, string lastName);
        Task DeleteAuthorAsync(Guid authorId);

    }
}
