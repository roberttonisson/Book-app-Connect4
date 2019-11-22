using System.Collections.Generic;
using Domain;

namespace WebApp.DTO
{
    public class BookIndexDto
    {
        public Book Book { get; set; } = default!;
        public int CommentCount { get; set; }
        public string? LastComment { get; set; }
    }
}