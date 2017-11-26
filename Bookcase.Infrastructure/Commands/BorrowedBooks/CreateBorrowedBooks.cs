using System;

namespace Bookcase.Infrastructure.Commands.BorrowedBooks
{
    public class CreateBorrowedBooks
    {
        public Guid BorrowedBooksId { get;  set; }
        public Guid BookId { get;  set; }
        public Guid UserId { get; set; }
        
    }
}
