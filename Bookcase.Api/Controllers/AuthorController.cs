using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Bookcase.Infrastructure.Commands.Author;
using Bookcase.Infrastructure.Services;
using WebApi.OutputCache.V2;
using System.Linq;

namespace Bookcase.Api.Controllers
{
    [AutoInvalidateCacheOutput]
    public class AuthorController: ApiController
    {
        private readonly IAuthorService _authorService;
        
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;

        }

        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        [Route("api/Author")]
        public async Task<IHttpActionResult> Get()
        {
            
            var author = await _authorService.GetAllAuthorsAsync();
            return Json(author);
        }

        [Route("api/Author/{name}")]
        public async Task<IHttpActionResult> Get(string name)
        {
            
            var author = await _authorService.BrowseAsync(name);
            if (author.Any())
            {
                return Json(author);
            }
            else
            {
                return NotFound();
            }
        }

        [Route("api/Author")]
        public async Task<IHttpActionResult> Post([FromBody]CreateUpdateAuthor command)
        {
            
            command.AuthorId = Guid.NewGuid();

            await _authorService.AddAuthorAsync(command.AuthorId, command.FirstName, command.LastName);
            return Created($"/api/Author/{command.AuthorId}", command);

        }

        [Route("api/Author/{AuthorId}")]
        public async Task<IHttpActionResult> Put(Guid authorId, [FromBody]CreateUpdateAuthor command)
        {
            await _authorService.EditAuthorAsync(authorId, command.FirstName, command.LastName);

            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("api/Author/{AuthorId}")]
        public async Task<IHttpActionResult> Delete(Guid authorId)
        {
            await _authorService.DeleteAuthorAsync(authorId);

            return StatusCode(HttpStatusCode.NoContent);

        }
    }
}