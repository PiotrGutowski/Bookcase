using Bookcase.ClientRepository.IRepositories;
using Bookcase.ClientRepository.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Bookcase.WebClient.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;

        public BookController(IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }


        [HttpGet]
        public async Task<ActionResult> Index(string name)
        {
            var book = await _bookRepository.GetBookByNameAsync(name);
            if (book == Enumerable.Empty<Book>())
            {
                ViewBag.Error = true;
                return View(book);
            }
            return View(book);
        }
        public async Task<ActionResult> CreateBook()
        {
            var author = await _authorRepository.GetAllAuthorsAsync();
            if (author == Enumerable.Empty<Author>())
            {
                ViewBag.Error = true;
                return View(author);
            }
            ViewBag.AuthorId = new SelectList(author, "AuthorId", "FullName");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateBook(Book book)
        {
            if (ModelState.IsValid)
            {
                await _bookRepository.AddAsync(book);
                return RedirectToAction("Index");
            }
            var author = await _authorRepository.GetAllAuthorsAsync();
            ViewBag.AuthorId = new SelectList(author, "AuthorId", "FullName");
            return View(book);
        }

        public async Task<ActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                throw new HttpException(400, "Bad Request");
            }
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book == null)
            {
                throw new HttpException(404, "Not Found");
            }
            var author = await _authorRepository.GetAllAuthorsAsync();
            if (author == Enumerable.Empty<Author>())
            {
                throw new HttpException(404, "Not Found");
            }
            ViewBag.AuthorId = new SelectList(author, "AuthorId", "FullName");
            return View(book);
        }
        [HttpPost]
        public async Task<ActionResult> Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                await _bookRepository.EditAsync(book);
                return RedirectToAction("Index");
            }
            var author = await _authorRepository.GetAllAuthorsAsync();
            ViewBag.AuthorId = new SelectList(author, "AuthorId", "FullName");
            return View(book);

        }

        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                throw new HttpException(400, "Bad Request");
            }
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book == null)
            {
                throw new HttpException(404, "Not Found");
            }
            return View(book);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                throw new HttpException(400, "Bad Request");
            }
            await _bookRepository.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}