using System;
using System.ComponentModel.DataAnnotations;

namespace Bookcase.Core.Domain
{
    public class BorrowedBooks
    {
        public Guid BorrowedBooksId { get; protected set; }
        public Guid BookId { get; protected set; }
        public Guid UserId { get; protected set; }

        [Display(Name = "Date of Borrow")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBorrow { get; protected set; }

        public Book Book { get; set; }
        public User User { get; set; }

        protected BorrowedBooks()
        {
            
        }

        public BorrowedBooks(Guid borrowedBooksId, Guid userId, Guid bookId)
        {
            BorrowedBooksId = borrowedBooksId;
            BookId = bookId;
            UserId = userId;
            DateOfBorrow = DateTime.UtcNow;
        }
    }
}
