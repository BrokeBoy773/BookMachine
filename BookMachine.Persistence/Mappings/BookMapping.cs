using BookMachine.Core.Interfaces.Persistence.Mappings;
using BookMachine.Core.Models;
using BookMachine.Persistence.Entities;

namespace BookMachine.Persistence.Mappings
{
    public class BookMapping : IBookMapping
    {
        public static List<Book> FromBookEntityListToBookList(List<BookEntity> bookEntities)
        {
            List<Book> books = [];

            foreach (BookEntity bookEntity in bookEntities)
            {
                Author author = Author.Create(bookEntity.AuthorId, bookEntity!.Author!.Name, []).Author;
                books.Add(Book.Create(bookEntity.BookId, bookEntity.Title, bookEntity.AuthorId, author).Book);
            }

            return books;
        }

        public static Book FromBookEntityToBook(List<BookEntity> bookEntities, BookEntity bookEntity)
        {
            Author? author = null;

            List<Book> books = [];
            author = Author.Create(bookEntity!.AuthorId, bookEntity!.Author!.Name, books).Author;

            foreach (var bookEnt in bookEntities)
            {
                books.Add(Book.Create(bookEnt.BookId, bookEnt.Title, bookEnt.AuthorId, author).Book);
            }

            Book? book = Book.Create(bookEntity.BookId, bookEntity.Title, bookEntity.AuthorId, author).Book;

            return book;
        }

        public static BookEntity FromBookToBookEntity(Book book)
        {
            BookEntity bookEntity = new()
            {
                BookId = book.BookId,
                Title = book.Title,

                AuthorId = book.AuthorId
            };

            return bookEntity;
        }
    }
}
