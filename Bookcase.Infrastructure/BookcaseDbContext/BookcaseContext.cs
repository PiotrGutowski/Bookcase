using System.Data.Entity;
using Bookcase.Core.Domain;
using System.Data.Common;

namespace Bookcase.Infrastructure.BookcaseDbContext
{
    public class BookcaseContext: DbContext
    {
        public BookcaseContext() : base("Bookcase")
        {

        }

        public BookcaseContext(DbConnection connection) : base(connection, true)
        {

        }


        public DbSet<Book> Book{ get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<BorrowedBooks> BorrowedBooks { get; set; }
        public DbSet<User> User { get; set; }
    }
}
