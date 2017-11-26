using Bookcase.ClientRepository.IRepositories;
using Bookcase.ClientRepository.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Bookcase.WebClient.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        [HttpGet]
        public async Task<ActionResult> Index(string name)
        {
            var user = await _userRepository.GetUserByNameAsync(name);
            if (user == Enumerable.Empty<User>())
            {
                ViewBag.Error = true;
                return View(user);
            }
            return View(user);
        }

        public ActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser(User user)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    await _userRepository.AddUserAsync(user);
                    return RedirectToAction("Index");
                }
                return View(user);
            }
            catch (HttpException)
            {
                throw (new HttpException(409, "Conflict"));
            }

        }

        public async Task<ActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                throw new HttpException(400, "Bad Request");
            }
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                throw new HttpException(404, "Not Found");
            }
            return View(user);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(User user)
        {
            if (ModelState.IsValid)
            {
                await _userRepository.EditAsync(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                throw new HttpException(400, "Bad Request");
            }
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                throw new HttpException(404, "Not Found");
            }
            return View(user);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                throw new HttpException(400, "Bad Request");
            }
            await _userRepository.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}