using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bookcase.Core.Domain;
using Bookcase.Core.Repositories;
using Bookcase.Infrastructure.Commands.BorrowedBooks;
using Bookcase.Infrastructure.DTO;
using Bookcase.Infrastructure.Extensions;
using AutoMapper;

namespace Bookcase.Infrastructure.Services
{
    public class BorrowedBooksService: IBorrowedBooksService
    {
        private readonly IBorrowedBooksRepository _borrowedBooksRepository;
        private readonly IMapper _mapper;

        public BorrowedBooksService(IBorrowedBooksRepository borrowedBooksRepository, IMapper mapper)
        {
            _borrowedBooksRepository = borrowedBooksRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BorrowedBooksDto>> GetAllBorrowedBookssAsync()
        {
            var borrowedBooks = await _borrowedBooksRepository.GetAllBorrowedBooksAsync();
            return _mapper.Map<IEnumerable<BorrowedBooksDto>>(borrowedBooks);
        }

        public async Task<IEnumerable<BorrowedBooksDto>> BrowseAsync(string name = null)
        {
            var borrowedBooks = await _borrowedBooksRepository.BrowseAsync(name);
            return _mapper.Map<IEnumerable<BorrowedBooksDto>>(borrowedBooks);

        }

        public async Task AddBorrowedBooksAsync(CreateBorrowedBooks createBorrowedBooks)
        {
            var borrowedBooks = await _borrowedBooksRepository.GetBorrowedBooksByIdAsync(createBorrowedBooks.BookId);
            if (borrowedBooks != null)
            {
                throw new Exception($"BorrowedBooks Id: '{createBorrowedBooks.BookId}' already exists.");
            }
            borrowedBooks = new BorrowedBooks(createBorrowedBooks.BorrowedBooksId, createBorrowedBooks.UserId, 
                createBorrowedBooks.BookId);
            await _borrowedBooksRepository.AddAsync(borrowedBooks);
        }

        public async Task DeleteBorrowedBooksAsync(Guid borrowedBooksId)
        {
            var borrowedBooks = await _borrowedBooksRepository.GetOrFailAsync(borrowedBooksId);
            await _borrowedBooksRepository.DeleteAsync(borrowedBooks);

        }
    }
}
