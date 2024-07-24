using BookMachine.Core.Validations;

namespace BookMachine.Core.Models
{
    public class Book
    {
        public const int MAX_TITLE_LENGTH = 256;

        public Guid BookId { get; }
        public string Title { get; } = string.Empty;
        public Guid AuthorId { get; set; }
        public Author? Author { get; set; }

        private Book(Guid bookId, string title)
        {
            BookId = bookId;
            Title = title;
        }

        public static (Book Book, List<string> Errors) Create(Guid bookId, string title)
        {
            List<string> errors = [];

            BookValidation.TitleValidation(title, errors);

            Book book = new(bookId, title);

            return (book, errors);
        }
    }
}
