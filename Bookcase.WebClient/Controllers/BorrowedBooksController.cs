using Bookcase.ClientRepository.IRepositories;
using Bookcase.ClientRepository.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Bookcase.WebClient.Controllers
{
    public class BorrowedBooksController : Controller
    {
        private readonly IBorrowedBooksRepository _borrowedBooksRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IUserRepository _userRepository;

        public BorrowedBooksController(IBorrowedBooksRepository borrowedBooksRepository,
                                     IBookRepository bookRepository, IUserRepository userRepository)
        {
            _borrowedBooksRepository = borrowedBooksRepository;
            _bookRepository = bookRepository;
            _userRepository = userRepository;
        }


        [HttpGet]
        public async Task<ActionResult> Index(string name)
        {
            var borrowedBooks = await _borrowedBooksRepository.GetBorrowedBooksByNameAsync(name);
            if (borrowedBooks == Enumerable.Empty<BorrowedBooks>())
            {
                ViewBag.Error = true;
                return View(borrowedBooks);
            }
            return View(borrowedBooks);
        }

        public async Task<ActionResult> CreateBorrowedBooks()
        {
            var allBook = await _bookRepository.GetAllBookAsync();
            if (allBook == Enumerable.Empty<Book>())
            {
                ViewBag.Error = true;
                return RedirectToAction("Index");
            }
            var book = (from b in allBook where b.IsAvailable == true select b).ToList();
            var user = await _userRepository.GetAllUserAsync();
            if (user == Enumerable.Empty<User>())
            {
                ViewBag.Error = true;
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(user, "UserId", "Name");
            ViewBag.BookId = new SelectList(book, "BookId", "Title");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateBorrowedBooks(BorrowedBooks borrowedBooks)
        {
            await _borrowedBooksRepository.AddAsync(borrowedBooks);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                throw new HttpException(400, "Bad Request");
            }
            var borrowedBooks = await _borrowedBooksRepository.GetBorrowedBooksByIdAsync(id);
            if (borrowedBooks == null)
            {
                throw new HttpException(404, "Not Found");
            }
            return View(borrowedBooks);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                throw new HttpException(400, "Bad Request");
            }
            await _borrowedBooksRepository.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}