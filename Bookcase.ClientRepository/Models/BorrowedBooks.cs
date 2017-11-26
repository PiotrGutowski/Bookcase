using System;
using System.ComponentModel.DataAnnotations;

namespace Bookcase.ClientRepository.Models
{
    public class BorrowedBooks
    {
        public Guid BorrowedBooksId { get;  set; }
        public Guid BookId { get;  set; }
        public Guid UserId { get; set; }

        [Display(Name = "Date of Borrow")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBorrow { get; set; }

        public Book Book { get; set; }
        public User User { get; set; }

    }
}
