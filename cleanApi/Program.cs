var configuration = GetConfiguration();
var builder = WebApplication.CreateBuilder(args);
Log.Logger = CreateSerilogLogger(configuration);

try
{
    Log.Information("Configuring Webhost ....");
    var host = CreateHostBuilder(args).Build();
    Log.Information("Applying Migration ....");
    host.MigrateDatabase<CleanContext>((context, services) => {
        var env = services.GetService<IWebHostEnvironment>();
        var logger = services.GetService<ILogger<CleanContextSeed>>();
        CleanContextSeed.SeedAsync(context, logger).Wait();
    });
    Log.Information("Starting Host ...");
    host.Run();
    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unexpected Error");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}

IHostBuilder CreateHostBuilder(string[] args) => 
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder => {
            webBuilder.UseStartup<Startup>();
        });

IConfiguration GetConfiguration()
{
    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
    
    return builder.Build();
}

Serilog.ILogger CreateSerilogLogger(IConfiguration configuration)
{
    return new LoggerConfiguration()
        .MinimumLevel.Verbose()
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .ReadFrom.Configuration(configuration)
        .CreateLogger();
}