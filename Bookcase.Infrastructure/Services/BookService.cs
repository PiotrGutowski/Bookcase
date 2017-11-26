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
   public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;

        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            var book = await _bookRepository.GetAllBookAsync();
            return _mapper.Map<IEnumerable<BookDto>>(book);
        }

        public async Task<BookDto> GetBookByNameAsync(string name)
        {
            var book = await _bookRepository.GetBookByNameAsync(name);
            return _mapper.Map<BookDto>(book);
        }

        public async Task<IEnumerable<BookDto>> BrowseAsync(string name = null)
        {
            var book = await _bookRepository.BrowseAsync(name);
            return _mapper.Map<IEnumerable<BookDto>>(book);

        }

        public async Task AddBookAsync(Guid bookId, Guid authorId, string title, string ISBN, DateTime published, bool isAvailable)
        {
            var book = await _bookRepository.GetBookByNameAsync(title);
         
            book = new Book(bookId, authorId, title, ISBN, published, isAvailable);
            await _bookRepository.AddAsync(book);
        }

        public async Task EditBookAsync(Guid bookId, Guid authorId, string title, string ISBN, DateTime published, bool isAvailable)
        {

            var book = await _bookRepository.GetOrFailAsync(bookId);
            
            book.SetTitle(title);
            book.SetISBN(ISBN);
            book.SetPublished(published);
            book.SetIsAvailable(isAvailable);
            await _bookRepository.EditAsync(book);
        }

        public async Task DeleteBookAsync(Guid bookId)
        {
            var book = await _bookRepository.GetOrFailAsync(bookId);
            await _bookRepository.DeleteAsync(book);

        }

    }
}
