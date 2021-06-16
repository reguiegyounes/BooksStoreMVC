using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStoreKhaled.Models.Repositories
{
    public class cBookRepository : IBooksStoreRepository<Book>
    {
        List<Book> books;
        public cBookRepository()
        {
            books = new List<Book>()
            {
                new Book{BookId=1,Title="c#" , Description="c# desc" ,ImageUrl="01.png" },
                new Book{BookId=2,Title="java" , Description="java desc",ImageUrl="02.png" },
                new Book{BookId=3,Title="pascal" , Description="pascal desc",ImageUrl="03.png" }
            };
        }
        public void add(Book newBook)
        {
            newBook.BookId = books.Max(b => b.BookId)+1;
            books.Add(newBook);
        }

        public void delete(int id)
        {
            var book = Find(id);
            books.Remove(book);
        }

        public IList<Book> List()
        {
            return books;
        }

        public void update(int id,Book newBook)
        {
            var book = Find(id);
            book.Title = newBook.Title;
            book.Description = newBook.Description;
            book.Author = newBook.Author;
            book.ImageUrl = newBook.ImageUrl;
        }

        public Book Find(int id)
        {
            var book = books.SingleOrDefault(b => b.BookId == id);
            return book;
        }
    }
}
