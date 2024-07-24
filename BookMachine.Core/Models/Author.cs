using BookMachine.Core.Validations;

namespace BookMachine.Core.Models
{
    public class Author
    {
        public const int MAX_NAME_LENGTH = 128;

        public Guid AuthorId { get; }
        public string Name { get; } = string.Empty;

        public List<Book> Books { get; } = [];

        private Author(Guid authorId, string name, List<Book> books)
        {
            AuthorId = authorId;
            Name = name;

            Books = books;
        }

        public static (Author Author, List<string> Errors) Create(Guid authorId, string name, List<Book> books)
        {
            List<string> errors = [];

            AuthorValidation.NameValidation(name, errors);

            Author author = new(authorId, name, books);

            return (author, errors);
        }

    }
}
