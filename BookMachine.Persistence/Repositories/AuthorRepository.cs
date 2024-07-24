using BookMachine.Core.Interfaces.Persistence.Repositories;
using BookMachine.Core.Models;
using BookMachine.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookMachine.Persistence.Repositories
{
    public class AuthorRepository(BookMachineDbContext context) : IAuthorRepository
    {
        private readonly BookMachineDbContext _context = context;

        public async Task<List<Author>> GetAllAuthorsAsync()
        {
            List<AuthorEntity> authorEntities = await _context.AuthorEntities
                .AsNoTracking()
                .ToListAsync();

            List<Author> authors = authorEntities
                .Select(a => Author.Create(a.AuthorId, a.Name).Author)
                .ToList();

            return authors;
        }
        public async Task<Author?> GetAuthorByIdAsync(Guid authorId)
        {
            AuthorEntity? authorEntity = await _context.AuthorEntities
                .Where(a => a.AuthorId == authorId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            Author? author = Author.Create(authorEntity!.AuthorId, authorEntity.Name).Author;

            return author;
        }

        public async Task<Guid> CreateAuthorAsync(Author author)
        {
            AuthorEntity authorEntity = new()
            {
                AuthorId = author.AuthorId,
                Name = author.Name,
            };

            await _context.AddAsync(authorEntity);
            await _context.SaveChangesAsync();

            return authorEntity.AuthorId;
        }

        public async Task<Guid> UpdateAuthorAsync(Guid authorId, Author author)
        {
            await _context.AuthorEntities.Where(a => a.AuthorId == authorId)
                .ExecuteUpdateAsync(x => x.SetProperty(y => y.Name, y => author.Name));

            return authorId;
        }

        public async Task<Guid> DeleteAuthorAsync(Guid authorId)
        {
            await _context.AuthorEntities.Where(a => a.AuthorId == authorId)
                .ExecuteDeleteAsync();

            return authorId;
        }
    }
}
