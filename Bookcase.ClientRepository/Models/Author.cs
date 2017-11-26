using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bookcase.ClientRepository.Models
{
    public class Author
    {
        public Guid AuthorId { get; set; }
        
        [Required(ErrorMessage = "First Name should be provided.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name should be provided.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

    }
}
