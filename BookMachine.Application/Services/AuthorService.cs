using BookMachine.Core.Interfaces.Application.Services;
using BookMachine.Core.Interfaces.Persistence.Repositories;
using BookMachine.Core.Models;

namespace BookMachine.Application.Services
{
    public class AuthorService(IAuthorRepository authorRepository) : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository = authorRepository;

        public async Task<List<Author>> GetAllAuthorsAsync()
        {
            return await _authorRepository.GetAllAuthorsAsync();
        }
        public async Task<Author?> GetAuthorByIdAsync(Guid authorId)
        {
            return await _authorRepository.GetAuthorByIdAsync(authorId);
        }

        public async Task<Guid> CreateAuthorAsync(Author author)
        {
            return await _authorRepository.CreateAuthorAsync(author);
        }

        public async Task<Guid> UpdateAuthorAsync(Guid authorId, Author author)
        {
            return await _authorRepository.UpdateAuthorAsync(authorId, author);
        }

        public async Task<Guid> DeleteAuthorAsync(Guid authorId)
        {
            return await _authorRepository.DeleteAuthorAsync(authorId);
        }
    }
}
