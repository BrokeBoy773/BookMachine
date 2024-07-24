using BookMachine.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookMachine.Persistence.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<BookEntity>
    {
        public void Configure(EntityTypeBuilder<BookEntity> builder)
        {
            builder.HasKey(b => b.BookId);

            builder
                .HasOne(b => b.Author)
                .WithMany(a => a.Books);
        }
    }
}
