namespace cleanInfrastructure.Context
{
    public class CleanContextDesignFactory : IDesignTimeDbContextFactory<CleanContext>
    {   
        public CleanContext CreateDbContext(string[] args)
        {
            var connection = "Server=localhost;Database=Clean1;User=root;Password=abe1234@;Port=3306;";
            
            var builderOptions = new DbContextOptionsBuilder<CleanContext>()
                .UseMySql(connection, ServerVersion.AutoDetect(connection));

            return new CleanContext(builderOptions.Options, new NoMediator());
        }

        class NoMediator : IMediator
        {
            public IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamRequest<TResponse> request, CancellationToken cancellationToken = default)
            {
                throw new NotImplementedException();
            }

            public IAsyncEnumerable<object> CreateStream(object request, CancellationToken cancellationToken = default)
            {
                throw new NotImplementedException();
            }

            public Task Publish(object notification, CancellationToken cancellationToken = default)
            {
                return Task.CompletedTask;
            }

            public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
            {
                return Task.CompletedTask;
            }

            public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
            {
                return Task.FromResult<TResponse>(default(TResponse));
            }

            public Task<object> Send(object request, CancellationToken cancellationToken = default)
            {
                return Task.FromResult(default(object));
            }
        }
    }
}