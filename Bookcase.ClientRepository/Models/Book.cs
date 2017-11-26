using System;
using System.ComponentModel.DataAnnotations;

namespace Bookcase.ClientRepository.Models
{
    public class Book
    {
        public Guid BookId { get; set; }

        public Guid AuthorId { get; set; }

        [Required(ErrorMessage = "Title should be provided.")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "ISBN should be provided.")]
        [Display(Name = "ISBN ")]
        [RegularExpression(@"^(97(8|9))?\d{9}(\d|X)$", ErrorMessage = "Invalid ISBN , example: 9784569874133 ")]
        public string ISBN { get; set; }

        [Required(ErrorMessage = "Date should be provided.")]
        [Display(Name = "Date of Publication")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Published { get; set; }

        [Display(Name = "Available")]
        public bool IsAvailable { get; set; }

        public Author Author {get; set; }
    }
}
