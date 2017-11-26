using System;
using System.ComponentModel.DataAnnotations;

namespace Bookcase.ClientRepository.Models
{
    public class User
    {
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Name should be provided.")]
        [Display(Name = "User name")]
        public string Name { get;  set; }

        [Required(ErrorMessage = "E-mail should be provided.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
