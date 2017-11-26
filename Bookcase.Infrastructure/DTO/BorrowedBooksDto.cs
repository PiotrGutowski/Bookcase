using System;

namespace Bookcase.Infrastructure.DTO
{
   public class BorrowedBooksDto
    {
        public Guid BorrowedBooksId { get; set; }
        public Guid BookId { get;  set; }
        public Guid UserId { get;  set; }
        public DateTime DateOfBorrow { get;  set; }
        public BookDto Book { get; set; }
        public UserDto User{ get; set; }

    }
}
