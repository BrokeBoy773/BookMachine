using BookMachine.Core.Validations;

namespace BookMachine.Core.Models
{
    public class Book
    {
        public const int MAX_TITLE_LENGTH = 256;

        public Guid BookId { get; }
        public string Title { get; } = string.Empty;

        public Guid AuthorId { get; }
        public Author? Author { get; }

        private Book(Guid bookId, string title, Guid authorId, Author? author)
        {
            BookId = bookId;
            Title = title;

            AuthorId = authorId;
            Author = author;
        }

        public static (Book Book, List<string> Errors) Create(Guid bookId, string title, Guid authorId, Author? author)
        {
            List<string> errors = [];

            BookValidation.TitleValidation(title, errors);

            Book book = new(bookId, title, authorId, author);

            return (book, errors);
        }
    }
}
