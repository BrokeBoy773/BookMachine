using BookMachine.Core.Models;

namespace BookMachine.Core.Interfaces.Persistence.Repositories
{
    public interface IAuthorRepository
    {
        public Task<List<Author>> GetAllAuthorsAsync();
        public Task<Author?> GetAuthorByIdAsync(Guid authorId);

        public Task<Guid> CreateAuthorAsync(Author author);

        public Task<Guid> UpdateAuthorAsync(Guid authorId, Author author);

        public Task<Guid> DeleteAuthorAsync(Guid authorId);
    }
}
