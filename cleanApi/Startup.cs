namespace cleanApi;
public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddInfrastructureServices(Configuration);
        services.AddApplicationServices();
        services.AddControllers(options => {
            options.Filters.Add(typeof(HttpGlobalExceptionFilter));
        });
        services.AddMvc()
            .AddNewtonsoftJson(n => {
                n.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
        
        services.AddSwaggerGen(c => {
            c.SwaggerDoc("v1", new OpenApiInfo{ Title = "Clean", Version = "v1" });
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if(env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Clean"));
        }

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endPoints => {
            endPoints.MapControllers();
        });
    }
}