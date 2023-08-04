namespace cleanInfrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("CleanDb");
            
            services.AddDbContext<CleanContext>(op => op.UseMySql(connection, ServerVersion.AutoDetect(connection)));
            services.AddScoped(typeof(IAsyncRepository<>), typeof(AsyncRepository<>));
            services.AddScoped<IStudentRepository, StudentRepository>();
            return services;
        }
    }
}