using BookMachine.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookMachine.Persistence.Configurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<AuthorEntity>
    {
        public void Configure(EntityTypeBuilder<AuthorEntity> builder)
        {
            builder.HasKey(a => a.AuthorId);

            builder
                .HasMany(a => a.Books)
                .WithOne(b => b.Author)
                .HasForeignKey(b => b.AuthorId);


        }
    }
}
