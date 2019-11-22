using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Book
    {
        public int BookId { get; set; }

        [MaxLength(128)]
        public string Title { get; set; } = default!;

        [MaxLength(1024)]
        public string? Summary { get; set; }

        public int PublishingYear { get; set; }
        public int AuthoredYear { get; set; }
        public int WordCount { get; set; }

        public int LanguageId { get; set; }
        public Language? Language { get; set; }


        public ICollection<Comment>? Comments { get; set; }

        public int PublisherId { get; set; }
        public Publisher? Publisher { get; set; }

        public ICollection<BookAuthor>? BookAuthors { get; set; }
    }
}