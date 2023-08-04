namespace cleanInfrastructure.Context
{
    public class CleanContext : DbContext
    {
        private readonly IMediator _mediator;
        public DbSet<Student> Students { get; set; }
        public DbSet<Sex> Sexes { get; set; }
        public CleanContext(DbContextOptions<CleanContext> options) : base(options)
        {
        }

        public CleanContext(DbContextOptions<CleanContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}