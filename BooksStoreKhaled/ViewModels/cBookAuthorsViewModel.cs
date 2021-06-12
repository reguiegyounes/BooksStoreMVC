using BooksStoreKhaled.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BooksStoreKhaled.ViewModels
{
    public class cBookAuthorsViewModel
    {
        public int BookId { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(3)]
        public string Title { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 3)]
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public List<Author> Authors { get; set; }
    }
}
