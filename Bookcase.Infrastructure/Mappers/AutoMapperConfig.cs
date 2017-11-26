using AutoMapper;
using Bookcase.Core.Domain;
using Bookcase.Infrastructure.DTO;

namespace Bookcase.Infrastructure.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
                  => new MapperConfiguration(cfg =>
                  {

                      cfg.CreateMap<Author, AuthorDto>();
                      cfg.CreateMap<Book, BookDto>();
                      cfg.CreateMap<BorrowedBooks, BorrowedBooksDto>();
                      cfg.CreateMap<User, UserDto>();
                  })
                  .CreateMapper();
    }
}
