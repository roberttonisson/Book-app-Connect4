using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Publisher
    {
        public int PublisherId { get; set; }

        [MaxLength(128)]
        public string PublisherName { get; set; } = default!;

        public ICollection<Book>? Books { get; set; }
    }
}