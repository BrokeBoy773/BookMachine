namespace BookMachine.Persistence.Entities
{
    public class BookEntity
    {
        public Guid BookId { get; set; }
        public string Title { get; set; } = string.Empty;
        public Guid AuthorId { get; set; }
        public AuthorEntity? Author { get; set; }
    }
}
