using BookMachine.Core.Interfaces.Persistence.Mappings;
using BookMachine.Core.Models;
using BookMachine.Persistence.Entities;

namespace BookMachine.Persistence.Mappings
{
    public class AuthorMapping : IAuthorMapping
    {
        public static List<Author> FromAuthorEntityListToAuthorList(List<AuthorEntity> authorEntities)
        {
            List<Author> authors = [];

            foreach (var authorEntity in authorEntities)
            {
                if (authorEntity.Books.Count != 0)
                {
                    List<Book> books = [];
                    Author author = Author.Create(authorEntity.AuthorId, authorEntity.Name, books).Author;

                    foreach (var bookEntity in authorEntity.Books)
                    {
                        books.Add(Book.Create(bookEntity.BookId, bookEntity.Title, bookEntity.AuthorId, author).Book);
                    }

                    authors.Add(author);
                }
                else
                {
                    Author author = Author.Create(authorEntity.AuthorId, authorEntity.Name, []).Author;
                    authors.Add(author);
                }
            }

            return authors;
        }

        public static Author FromAuthorEntityToAuthor(AuthorEntity authorEntity)
        {
            Author? author = null;

            if (authorEntity!.Books.Count != 0)
            {
                List<Book> books = [];
                author = Author.Create(authorEntity.AuthorId, authorEntity.Name, books).Author;

                foreach (var bookEntity in authorEntity!.Books)
                {
                    books.Add(Book.Create(bookEntity.BookId, bookEntity.Title, bookEntity.AuthorId, author).Book);
                }
            }
            else
            {
                author = Author.Create(authorEntity.AuthorId, authorEntity.Name, []).Author;
            }

            return author;
        }

        public static AuthorEntity FromAuthorToAuthorEntity(Author author)
        {
            AuthorEntity authorEntity = new()
            {
                AuthorId = author.AuthorId,
                Name = author.Name,
            };

            return authorEntity;
        }
    }
}
