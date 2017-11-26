using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Bookcase.Infrastructure.Commands.Book;
using Bookcase.Infrastructure.Services;
using WebApi.OutputCache.V2;
using System.Linq;

namespace Bookcase.Api.Controllers
{
    [AutoInvalidateCacheOutput]
    public class BookController: ApiController
    {
        private readonly IBookService _bookService;
       

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
            
        }

        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        [Route("api/Book")]
        public async Task<IHttpActionResult> Get()
        {
            var book = await _bookService.GetAllBooksAsync();
            if (book.Any())
            {
                return Json(book);
            }
            else
            {
                return NotFound();
            }
        }

        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        [Route("api/Book/{name}")]
        public async Task<IHttpActionResult> Get(string name)
        {
            var book = await _bookService.BrowseAsync(name);

            if (book.Any())
            {
                return Json(book);
            }
            else
            {
                return NotFound();
            }
        }
        [Route("api/book")]
        public async Task<IHttpActionResult> Post([FromBody]CreateUpdateBook command)
        {

            command.BookId = Guid.NewGuid();
            await _bookService.AddBookAsync(command.BookId, command.AuthorId, command.Title, command.ISBN,
                                            command.Published, command.IsAvailable);
            return Created($"/api/Book/{command.BookId}", command);

        }
        [Route("api/Book/{BookId}")]
        public async Task<IHttpActionResult> Put(Guid bookId, [FromBody]CreateUpdateBook command)
        {
            await _bookService.EditBookAsync(bookId, command.AuthorId, command.Title, command.ISBN,
                command.Published, command.IsAvailable);

            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("api/Book/{BookId}")]
        public async Task<IHttpActionResult> Delete(Guid bookId)
        {
            await _bookService.DeleteBookAsync(bookId);

            return StatusCode(HttpStatusCode.NoContent);

        }
    }
}