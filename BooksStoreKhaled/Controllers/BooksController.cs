using BooksStoreKhaled.Models;
using BooksStoreKhaled.Models.Repositories;
using BooksStoreKhaled.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;

namespace BooksStoreKhaled.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBooksStoreRepository<Book> bookRepository;
        private readonly IBooksStoreRepository<Author> authorRepository;
        private readonly IHostingEnvironment hosting;

        public BooksController(IBooksStoreRepository<Book> bookRepository, IBooksStoreRepository<Author> authorRepository, IHostingEnvironment hosting)
        {
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
            this.hosting = hosting;
        }
        // GET: BookController
        public ActionResult Index()
        {
            var books = bookRepository.List();
            return View(books);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            var book = bookRepository.Find(id);
            return View(book);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            var model = new cBookAuthorsViewModel
            {
                Authors = authorRepository.List().ToList()
            };
            return View(model);
        }

        // POST: BookController/Create
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 1024*1024*1024)] //byte
        [ValidateAntiForgeryToken]
        public ActionResult Create(cBookAuthorsViewModel model)
        {
            var vModel = new cBookAuthorsViewModel
            {
                Authors = authorRepository.List().ToList()
            };
            if (ModelState.IsValid)
            {
                try
                {
                    FileInfo info = new FileInfo(model.Image.FileName);
                    string fileName = model.Title + info.Extension;
                    if (model.Image!=null)
                    {
                        string uploads = Path.Combine(hosting.WebRootPath,"uploads");
                        string fullPath = Path.Combine(uploads,fileName);
                        using (FileStream fileStream= System.IO.File.Create(fullPath))
                        {
                            model.Image.CopyTo(fileStream);
                            fileStream.Flush();
                        }
                    }
                    var author = authorRepository.Find(model.AuthorId);
                    Book book = new Book
                    {
                        BookId = model.BookId,
                        Title = model.Title,
                        Description = model.Description,
                        ImageUrl=fileName,
                        Author = author
                    };
                    bookRepository.add(book);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            ModelState.AddModelError("","You have to fill all required fields");
            return View(vModel);
            
           
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {

            var book = bookRepository.Find(id);
            var AuthorID = book.Author == null ? 1 : book.Author.AuthorId;
            var model = new cBookAuthorsViewModel
            {
                BookId = book.BookId,
                Title = book.Title,
                Description = book.Description,
                AuthorId = AuthorID,
                Authors = authorRepository.List().ToList(),
                ImageUrl = book.ImageUrl
            };
            return View(model);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 1024 * 1024 * 1024)] //byte
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, cBookAuthorsViewModel model)
        {
            var vModel = new cBookAuthorsViewModel
            {
                Authors = authorRepository.List().ToList()
            };
            if (ModelState.IsValid)
            {
                try
                {
                    string fileName= bookRepository.Find(id).ImageUrl;
                    string uploads = Path.Combine(hosting.WebRootPath, "uploads");
                    string oldPath = Path.Combine(uploads, fileName);
                    string fullPath ;
                    if (model.Image != null)
                    {
                        FileInfo info = new FileInfo(model.Image.FileName);
                        fileName = model.Title + info.Extension;
                        fullPath = Path.Combine(uploads, fileName);
                        if (System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath);
                        }
                        using (FileStream fileStream = System.IO.File.Create(fullPath))
                        {
                            model.Image.CopyTo(fileStream);
                            fileStream.Flush();
                        }
                    }
                    else
                    {
                        if (System.IO.File.Exists(oldPath))
                        {
                            FileInfo info = new FileInfo(oldPath);
                            fileName = model.Title + info.Extension;
                            fullPath = Path.Combine(uploads, fileName);
                            System.IO.File.Move(oldPath, fullPath);
                        }
                    }
                    var author = authorRepository.Find(model.AuthorId);
                    Book book = new Book
                    {
                        BookId = model.BookId,
                        Title = model.Title,
                        Description = model.Description,
                        Author = author,
                        ImageUrl = fileName
                    };
                    bookRepository.update(id, book);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            ModelState.AddModelError("", "You have to fill all required fields");
            return View(vModel);
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            var book = bookRepository.Find(id);
            return View(book);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostDelete(int id)
        {
            try
            {
                bookRepository.delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
