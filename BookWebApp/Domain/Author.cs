using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Domain
{
    public class Author
    {
        public int AuthorId { get; set; }

        [MaxLength(128)] public string FirstName { get; set; } = default!;
        [MaxLength(128)] public string LastName { get; set; } = default!;

        public int YearOfBirth { get; set; }

        public ICollection<BookAuthor>? AuthorBooks { get; set; }

        public string FirstLastName => FirstName + " " + LastName;
        public string LastFirstName => LastName + " " + FirstName;
        
    }
}