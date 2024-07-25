using BookMachine.Core.Interfaces.Persistence.Repositories;
using BookMachine.Core.Models;
using BookMachine.Persistence.Entities;
using BookMachine.Persistence.Mappings;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookMachine.Persistence.Repositories
{
    public class AuthorRepository(BookMachineDbContext context) : IAuthorRepository
    {
        private readonly BookMachineDbContext _context = context;

        public async Task<List<Author>> GetAllAuthorsAsync()
        {
            List<AuthorEntity> authorEntities = await _context.AuthorEntities
                .AsNoTracking()
                .Include(a => a.Books)
                .ToListAsync();

            return AuthorMapping.FromAuthorEntityListToAuthorList(authorEntities);
        }

        public async Task<List<Author>> GetAuthorByFilterAsync(string? search, string? sortItem, string? sortOrder)
        {
            IQueryable<AuthorEntity> authorEntityQuery = _context.AuthorEntities
                .AsNoTracking()
                .Where(a => string.IsNullOrWhiteSpace(search) || a.Name.ToLower().Contains(search.ToLower()))
                .Include(a => a.Books);

            Expression<Func<AuthorEntity, object>> selectorKey;

            switch (sortItem)
            {
                case "name":
                    selectorKey = authorEntity => authorEntity.Name;
                    break;

                case "id":
                    selectorKey = authorEntity => authorEntity.AuthorId;
                    break;

                default:
                    goto case "id";
            }

            switch (sortOrder)
            {
                case "desc":
                    authorEntityQuery = authorEntityQuery.OrderByDescending(selectorKey);
                    break;

                case "asc":
                    authorEntityQuery = authorEntityQuery.OrderBy(selectorKey);
                    break;

                default:
                    goto case "asc";
            }

            List<AuthorEntity> authorEntities = await authorEntityQuery.ToListAsync();

            return AuthorMapping.FromAuthorEntityListToAuthorList(authorEntities);

        }

        public async Task<Author?> GetAuthorByIdAsync(Guid authorId)
        {
            AuthorEntity? authorEntity = await _context.AuthorEntities
                .AsNoTracking()
                .Where(a => a.AuthorId == authorId)
                .Include(a => a.Books)
                .FirstOrDefaultAsync();

            return AuthorMapping.FromAuthorEntityToAuthor(authorEntity!);
        }

        public async Task<Guid> CreateAuthorAsync(Author author)
        {
            AuthorEntity authorEntity = AuthorMapping.FromAuthorToAuthorEntity(author);

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
