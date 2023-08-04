namespace cleanDomain.Common
{
    public abstract class EntityBase
    {
        public Guid Id { get; protected set; }
        public DateTime CreatedDate { get; protected set; }
        public DateTime LastModifiedDate { get; protected set; }

        public EntityBase()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.Now;
        }
    }
}