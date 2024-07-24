namespace BookMachine.Persistence.Entities
{
    public class AuthorEntity
    {
        public Guid AuthorId { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<BookEntity> Books { get; set; } = [];
    }
}
