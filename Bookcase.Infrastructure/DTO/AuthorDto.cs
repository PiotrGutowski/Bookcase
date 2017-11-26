using System;

namespace Bookcase.Infrastructure.DTO
{
    public class AuthorDto
    {
        public Guid AuthorId { get;  set; }
        public string FirstName { get; set; }
        public string LastName { get;  set; }
    }
}
