using System;

namespace Bookcase.Infrastructure.DTO
{
   public class BookDto
    {
        public Guid BookId { get; set; }
        public Guid AuthorId { get;  set; }
        public string Title { get;  set; }
        public string ISBN { get; set; }
        public DateTime Published { get;  set; }
        public bool IsAvailable { get; set; }
        public AuthorDto Author { get; set; }
    }
}
