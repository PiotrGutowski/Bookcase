namespace Bookcase.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Bookcase.Infrastructure.BookcaseDbContext.BookcaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Bookcase.Infrastructure.BookcaseDbContext.BookcaseContext";
        }

        protected override void Seed(Bookcase.Infrastructure.BookcaseDbContext.BookcaseContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
