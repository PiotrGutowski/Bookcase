using System;

namespace Bookcase.Infrastructure.Commands.Book
{
    public class CreateUpdateBook
    {
        public Guid BookId { get;  set; }
        public Guid AuthorId { get;  set; }
        public string Title { get;  set; }
        public string ISBN { get; set; }
        public DateTime Published { get;  set; }
        public bool IsAvailable { get;  set; }

    }
}
