using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookcase.Core.Domain
{
   public class Author
    {
        public Guid AuthorId { get; protected set; }

        [Display(Name = "First Name")]
        public string FirstName { get; protected set; }

        [Display(Name = "Last Name")]
        public string LastName { get; protected set; }

        [NotMapped]
        [Display(Name = "Full Name")]
        public string FullName => $"{FirstName} {LastName}";

        public ICollection<Book> Books { get; set; }

        protected Author()
        {
        }

        public Author(Guid authorId, string firstName, string lastName)
        {
            AuthorId = authorId;
            SetFirstName(firstName);
            SetLastName(lastName);
        }

        public void SetFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new Exception($"Author with id: '{AuthorId}' can not have an empty name.");
            }
            FirstName = firstName;

        }

        public void SetLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new Exception($"Author with id: '{AuthorId}' can not have an empty name.");
            }
            LastName = lastName;
        }
    }
}
