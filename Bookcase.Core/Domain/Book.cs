using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Bookcase.Core.Domain
{
   public class Book
    {
        public Guid BookId { get; protected set; }

        public Guid AuthorId { get; protected set; }

        [Display(Name = "User name")]
        public string Title { get; protected set; }

        [Display(Name = "ISBN ")]
        public string ISBN { get; protected set; }

        [Display(Name = "Date of Publication")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Published { get; protected set; }

        [Display(Name = "Available")]
        public bool IsAvailable { get; protected set; }

        public Author Author { get; set; }

        protected Book()
        {
                
        }

        public Book(Guid bookId, Guid authorId, string title, string ISBN, DateTime published, bool isAvailable)
        {
            BookId = bookId;
            AuthorId = authorId;
            SetTitle(title);
            SetISBN(ISBN);
            SetPublished(published);
            SetIsAvailable(isAvailable);
        }

        public void SetTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new Exception($"Book with id: '{BookId}' can not have an empty name.");
            }
            Title = title;
        }

        public void SetISBN(string ISBN)
        {
            if (string.IsNullOrWhiteSpace(ISBN))
            {
                throw new Exception($"Book with id: '{BookId}' can not have an empty ISBN.");
            }
            var match = Regex.Match(ISBN, @"^(97(8|9))?\d{9}(\d|X)$", RegexOptions.IgnoreCase);
            if (!match.Success)
            {
                throw new Exception($"eror");
            }
            this.ISBN = ISBN;
        }
       
        public void SetPublished(DateTime published)
        {
            if (published == DateTime.MinValue)
            {
                throw new Exception($"Book with id: '{BookId}' can not have an empty Published." );
            }
            Published = published;
        }

        public void SetIsAvailable(bool isAvailable)
        {
            IsAvailable = isAvailable;
        }
    }
}
