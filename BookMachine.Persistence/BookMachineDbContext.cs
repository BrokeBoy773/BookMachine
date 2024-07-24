using BookMachine.Persistence.Configurations;
using BookMachine.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookMachine.Persistence
{
    public class BookMachineDbContext : DbContext
    {
        public DbSet<BookEntity> BookEntities { get; set; }
        public DbSet<AuthorEntity> AuthorEntities { get; set; }

        public BookMachineDbContext(DbContextOptions<BookMachineDbContext> options) : base(options)
        {
            Database.EnsureCreated(); // Временное решение //
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
