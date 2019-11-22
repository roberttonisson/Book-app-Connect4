using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Comment
    {
        public int CommentId { get; set; }

        [MaxLength(1024)]
        public string CommentText { get; set; } = default!;

        public int BookId { get; set; }
        public Book? Book { get; set; }
    }
}