using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Bookcase.Infrastructure.Commands.BorrowedBooks;
using Bookcase.Infrastructure.Services;
using WebApi.OutputCache.V2;
using System.Linq;

namespace Bookcase.Api.Controllers
{
    
    [AutoInvalidateCacheOutput]
    public class BorrowedBooksController: ApiController
    {
        private readonly IBorrowedBooksService _borrowedBooksService;


        public BorrowedBooksController(IBorrowedBooksService borrowedBooksService)
        {
            _borrowedBooksService = borrowedBooksService;

        }

        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        [Route("api/BorrowedBooks")]
        public async Task<IHttpActionResult> Get()
        {
            var borrowedBooks = await _borrowedBooksService.GetAllBorrowedBookssAsync();
            if (borrowedBooks.Any())
            {
                return Json(borrowedBooks);
            }
            else
            {
                return NotFound();
            }
        }

        [Route("api/BorrowedBooks/{name}")]
        public async Task<IHttpActionResult> Get(string name)
        {
            var borrowedBooks = await _borrowedBooksService.BrowseAsync(name);
            if (borrowedBooks.Any())
            {
                return Json(borrowedBooks);
            }
            else
            {
                return NotFound();
            }
        }

        [InvalidateCacheOutput("Get", typeof(BookController))]
        [Route("api/borrowedbooks")]
        public async Task<IHttpActionResult> Post([FromBody]CreateBorrowedBooks command)
        {
            command.BorrowedBooksId = Guid.NewGuid();
            await _borrowedBooksService.AddBorrowedBooksAsync(command);
            return Created($"/api/BorrowedBooks/{command.BorrowedBooksId}", command);
        }

        [Route("api/BorrowedBooks/{BorrowedBooksId}")]
        public async Task<IHttpActionResult> Delete(Guid borrowedBooksId)
        {
            await _borrowedBooksService.DeleteBorrowedBooksAsync(borrowedBooksId);

            return StatusCode(HttpStatusCode.NoContent);

        }
    }
}