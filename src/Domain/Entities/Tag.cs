namespace Domain.Entities
{
    public class Tag : BaseAuditableEntity
    {
        private string name;
        public string Name { get => name.ToLower(); set => name = value.ToLower(); }

        public ICollection<Snippet> Snippets { get; set; } = [];
    }
}
