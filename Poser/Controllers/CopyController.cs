using Bogus.DataSets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Poser.Data;
using Poser.SignalR;

namespace Poser.Controllers
{
    public class CopyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
/*
using BookStore.Data;
using BookStore.Models;
using BookStore.Models.Repositories;
using BookStore.SignalR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookStoreRepository<Book> bookRepository;
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<MyHub> _hubContext;


        public BookController(IBookStoreRepository<Book> bookRepository, ApplicationDbContext context, IHubContext<MyHub> hubContext)
        {
            this.bookRepository = bookRepository;
            _context = context;
            _hubContext = hubContext;
        }

        public ActionResult Index()
        {
            return RedirectToAction("NoteFound");
        }

        public ActionResult NoteFound()
        {
            return View();
        }

        //Book Modal Partials
        public IActionResult CreateBook()
        {
            return PartialView("Partials/_CreateBookPartial");
        }

        [HttpPost]
        public ActionResult CreateAction(Book newBook)
        {
            if (newBook.Name == null)
            {
                return Json(new { success = false, message = "Please Fill All Fields" });
            }

            Book dataBook = new()
            {
                Name = newBook.Name,
                Title = newBook.Title,
                Summary = newBook.Summary
            };

            _context.Books.Add(dataBook);
            _context.SaveChanges();

            return Json(new { success = true, message = "Book Created Successfully" });
        }

        public IActionResult EditBook(int id)
        {
            return PartialView("Partials/_EditBookPartial", _context.Books.Find(id));
        }

        [HttpPost]
        public ActionResult UpdateAction(Book book)
        {
            var existingBook = _context.Books.Find(book.Id);
            if (existingBook == null)
            {
                return Json(new { success = false, message = "Book Didn't Updated successfully Or Not Found!" });
            }

            // Update specific properties of the existingBook
            existingBook.Name = book.Name;
            existingBook.Title = book.Title;
            existingBook.Summary = book.Summary;

            _context.Entry(existingBook).State = EntityState.Modified;
            _context.SaveChanges();

            return Json(new { success = true, message = "Book Updated successfully" });
        }

        public ActionResult DeleteBook(int id)
        {
            return PartialView("Partials/_DeleteBookPartial", _context.Books.Find(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            var book = _context.Books.Find(id);

            if (book == null)
            {
                return Json(new { success = false, message = "Book not found" });
            }

            _context.Books.Remove(book);
            _context.SaveChanges();

            await _hubContext.Clients.All.SendAsync("RefreshBookData");

            return Json(new { success = true, message = "Book deleted successfully" });
        }

        public JsonResult GetJsonData()
        {
            var books = _context.Books;

            var JsonBooks = Json(books.ToList());

            var JsonBooks2 = Json(books.ToList());

            var y = 9;

            return books == null ? Json(JsonBooks) : Json(JsonBooks);
        }

        public ActionResult Details(int id)
        {
            var book = _context.Books
                       .Include(b => b.Author)
                       .Include(b => b.BookLists)
                       .FirstOrDefault(b => b.Id == id);

            return book == null ? RedirectToAction("NoteFound") : View(book);
        }

        public void SeedData()
        {
            Data.Seeders.BookSeeder.SeedData(_context);
        }

        //public JsonResult PullGithubWebhook()
        //{
        //    var username = "babikerMakkawi";
        //    var password = "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";
        //    var cmd = "git pull https://" + username + ":" + password + "@github.com/" 
        //        + username + "/dotnet.core.BookStore.git";

        //    return System.Diagnostics.Process.Start(cmd) != null 
        //        ? Json(new { success = true, }) 
        //        : Json(new { success = false, });
        //}
    }
}
*/