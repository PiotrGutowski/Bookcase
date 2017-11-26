using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bookcase.Core.Domain;
using Bookcase.Core.Repositories;
using Bookcase.Infrastructure.DTO;
using Bookcase.Infrastructure.Extensions;
using AutoMapper;

namespace Bookcase.Infrastructure.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorService(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync()
        {
            var author = await _authorRepository.GetAllAuthorAsync();
            return _mapper.Map<IEnumerable<AuthorDto>>(author);
        }

        public async Task<AuthorDto> GetAuthorByNameAsync(string name)
        {
            var author = await _authorRepository.GetAuthorByNameAsync(name);
            return _mapper.Map<AuthorDto>(author);
        }

        public async Task<IEnumerable<AuthorDto>> BrowseAsync(string name = null)
        {
            var author = await _authorRepository.BrowseAsync(name);
            return _mapper.Map<IEnumerable<AuthorDto>>(author);
        }

        public async Task AddAuthorAsync(Guid authorId, string firstName, string lastName)
        {
            
            var author = new Author( authorId, firstName, lastName);
            await _authorRepository.AddAsync(author);
        }

        public async Task EditAuthorAsync(Guid authorId, string firstName, string lastName)
        {
            var author = await _authorRepository.GetOrFailAsync(authorId);
            author.SetFirstName(firstName);
            author.SetLastName(lastName);
            await _authorRepository.EditAsync(author);
        }

        public async Task DeleteAuthorAsync(Guid authorId)
        {
            var author = await _authorRepository.GetOrFailAsync(authorId);
            await _authorRepository.DeleteAsync(author);
        }
    }
}
