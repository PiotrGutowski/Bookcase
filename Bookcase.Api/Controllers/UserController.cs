using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Bookcase.Infrastructure.Commands.User;
using Bookcase.Infrastructure.Services;
using WebApi.OutputCache.V2;
using System.Linq;
using System.Web;
using System.Net.Http;

namespace Bookcase.Api.Controllers
{
    [AutoInvalidateCacheOutput]
    public class UserController: ApiController
    {
        private readonly IUserService _userService;


        public UserController(IUserService userService)
        {
            _userService = userService;

        }

        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        [Route("api/User")]
        public async Task<IHttpActionResult> Get()
        {
            var user= await _userService.GetAllUserAsync();
            return Json(user);
        }

        [Route("api/user/{name}")]
        public async Task<IHttpActionResult> Get(string name)
        {
            var user = await _userService.GetUserByNameAsync(name);
            if (user.Any())
            {
                return Json(user);
            }
            else
            {
                return NotFound();
            }
        }

        [Route("api/user")]
        public async Task<IHttpActionResult> Post([FromBody]CreateUpdateUser command)
        {
            try
            {
                await _userService.AddUserAsync(Guid.NewGuid(),
                    command.Email, command.Name);

                return Created($"/user/{command.UserId}", User);
            }
            catch(Exception ex)
            {
                throw new HttpResponseException( Request.CreateErrorResponse(HttpStatusCode.Conflict, ex.ToString()));
            }
        }

        [Route("api/user/{UserId}")]
        public async Task<IHttpActionResult> Put(Guid userId, [FromBody]CreateUpdateUser command)
        {
            await _userService.EditUserAsync(userId, command.Email, command.Name);
            

            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("api/user/{UserId}")]
        public async Task<IHttpActionResult> Delete(Guid userId)
        {
            await _userService.DeleteUserAsync(userId);

            return StatusCode(HttpStatusCode.NoContent);

        }
    }
}