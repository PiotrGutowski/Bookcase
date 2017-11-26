using System;
using System.Threading.Tasks;
using Bookcase.Core.Domain;
using Bookcase.Core.Repositories;

namespace Bookcase.Infrastructure.Extensions
{
    public static class RepositoryExtensions
    {
        public static async Task<Book> GetOrFailAsync(this IBookRepository repository, Guid bookId)
        {
            var book = await repository.GetBookByIdAsync(bookId);
            if (bookId == null)
            {
                throw new Exception($"Book with Id: '{bookId}' does not exists.");
            }

            return book;
        }

        public static async Task<Author> GetOrFailAsync(this IAuthorRepository repository, Guid authorId)
        {
            var author = await repository.GetAuthorByIdAsync(authorId);
            if (authorId == null)
            {
                throw new Exception($"Author with Id: '{authorId}' does not exists.");
            }

            return author;
        }

        public static async Task<BorrowedBooks> GetOrFailAsync(this IBorrowedBooksRepository repository, Guid borrowedBooksId)
        {
            var borrowedBooks = await repository.GetBorrowedBooksByIdAsync(borrowedBooksId);
            if (borrowedBooksId == null)
            {
                throw new Exception($"BorrowedBooks with Id: '{borrowedBooksId}' does not exists.");
            }

            return borrowedBooks;
        }
        public static async Task<User> GetOrFailAsync(this IUserRepository repository, Guid id)
        {
            var user = await repository.GetAsync(id);
            if (user == null)
            {
                throw new Exception($"User with id: '{id}' does not exist.");
            }

            return user;
        }
    }
}
