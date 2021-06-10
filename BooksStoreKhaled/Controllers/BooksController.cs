using BooksStoreKhaled.Models;
using BooksStoreKhaled.Models.Repositories;
using BooksStoreKhaled.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStoreKhaled.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBooksStoreRepository<Book> bookRepository;
        private readonly IBooksStoreRepository<Author> authorRepository;

        public BooksController(IBooksStoreRepository<Book> bookRepository , IBooksStoreRepository<Author> authorRepository)
        {
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
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
            var book=bookRepository.Find(id);
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
        [ValidateAntiForgeryToken]
        public ActionResult Create(cBookAuthorsViewModel model)
        {
            try
            {
                var author = authorRepository.Find(model.AuthorId);
                Book book = new Book
                {
                    BookId=model.BookId,
                    Title=model.Title,
                    Description=model.Description,
                    Author=author
                };
                bookRepository.add(book);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
