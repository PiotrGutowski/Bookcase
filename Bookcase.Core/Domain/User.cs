using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Bookcase.Core.Domain
{
    public class User
    {
        public Guid UserId { get; protected set; }

        [Display(Name = "User name")]
        public string Name { get; protected set; }

        [Display(Name = "Email")]
        public string Email { get; protected set; }

        protected User()
        {
                      
        }

        public User(Guid userId, string email, string name)
        {
            UserId = userId;
            SetEmail(email);
            SetName(name); 
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new Exception($"User  can not have an empty name.");
            }
            Name = name;
        }

        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new Exception($"User an not have an empty email.");
            }
            var match = Regex.Match(email, @"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$", RegexOptions.IgnoreCase);
            if (!match.Success)
            {
                throw new Exception($"Please Enter Correct Email Address");
            }
            Email = email;
        } 
    }
}
