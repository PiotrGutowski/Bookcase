using Bookcase.ClientRepository.IRepositories;
using Bookcase.ClientRepository.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Bookcase.WebClient.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }


        [HttpGet]
        public async Task<ActionResult> Index(string name)
        {

            var author = await _authorRepository.GetAuthorByNameAsync(name);
            if (author == Enumerable.Empty<Author>())
            {
                ViewBag.Error = true;
                return View(author);
            }

            return View(author);

        }

        public ActionResult CreateAuthor()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateAuthor(Author author)
        {

            if (ModelState.IsValid)
            {
                await _authorRepository.AddAsync(author);
                return RedirectToAction("Index");
            }
            return View(author);
        }

        public async Task<ActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                throw new HttpException(400, "Bad Request");
            }
            var author = await _authorRepository.GetAuthorByIdAsync(id);
            if (author == null)
            {
                throw new HttpException(404, "Not Found");
            }
            return View(author);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Author author)
        {
            if (ModelState.IsValid)
            {
                await _authorRepository.EditAsync(author);
                return RedirectToAction("Index");
            }
            return View(author);
        }

        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                throw new HttpException(400, "Bad Request");
            }
            var author = await _authorRepository.GetAuthorByIdAsync(id);
            if (author == null)
            {
                throw new HttpException(404, "Not Found");
            }
            return View(author);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                throw new HttpException(400, "Bad Request");
            }
            await _authorRepository.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}