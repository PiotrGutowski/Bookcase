using System;

namespace Bookcase.Infrastructure.Commands.User
{
   public class CreateUpdateUser
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        
    }
}
