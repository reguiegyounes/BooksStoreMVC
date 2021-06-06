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
                new Book{BookId=1,Title="c#" , Description="c# desc" },
                new Book{BookId=1,Title="java" , Description="java desc" },
                new Book{BookId=1,Title="pascal" , Description="pascal desc" }
            };
        }
        public void add(Book newBook)
        {
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
        }

        public Book Find(int id)
        {
            var book = books.SingleOrDefault(b => b.BookId == id);
            return book;
        }
    }
}
