using System.Collections.Generic;

namespace Domain
{
    public class Language
    {
        public int LanguageId { get; set; }

        public string LanguageName { get; set; } = default!;

        public ICollection<Book>? Books { get; set; }
    }
}