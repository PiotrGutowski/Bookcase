using System;

namespace Bookcase.Infrastructure.DTO
{
    public  class UserDto
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

    }
}
