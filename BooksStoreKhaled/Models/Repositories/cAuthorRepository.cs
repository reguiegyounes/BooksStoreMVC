using System.Collections.Generic;
using System.Linq;

namespace BooksStoreKhaled.Models.Repositories
{
    public class cAuthorRepository : IBooksStoreRepository<Author>
    {
        List<Author> authors;
        public cAuthorRepository()
        {
            authors = new List<Author>()
            {
                new Author{AuthorId=1,FullName="ali"  },
                new Author{AuthorId=2,FullName="mohamed" },
                new Author{AuthorId=3,FullName="walid" }
            };
        }
        public void add(Author newAuthor)
        {
            newAuthor.AuthorId = authors.Max(a => a.AuthorId) + 1;
            authors.Add(newAuthor);
        }

        public void delete(int id)
        {
            var author = Find(id);
            authors.Remove(author);
        }

        public IList<Author> List()
        {
            return authors;
        }

        public void update(int id, Author newAuthor)
        {
            var author = Find(id);
            author.FullName = newAuthor.FullName;
        }

        public Author Find(int id)
        {
            var author = authors.SingleOrDefault(a => a.AuthorId == id);
            return author;
        }
    }
}
