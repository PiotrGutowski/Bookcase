using System;

namespace Bookcase.Infrastructure.Commands.Author
{
    public  class CreateUpdateAuthor
    {
        public Guid AuthorId { get;  set; }
        public string FirstName { get;  set; }
        public string LastName { get; set; }
    }
}
