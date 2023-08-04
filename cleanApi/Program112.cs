// var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddControllers(op => {
//     op.Filters.Add(typeof(HttpGlobalExceptionFilter));
// });

// builder.Services.AddMvc()
//     .AddNewtonsoftJson(x => {
//         x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
//     });

// builder.Services.AddInfrastructureServices(builder.Configuration);
// builder.Services.AddApplicationServices();

// builder.Services.AddSwaggerGen(c => {
//     c.SwaggerDoc("v1", new OpenApiInfo { Title = "Clean", Version = "v1"});
// });

// Log.Logger = CreateSerilogLogger(builder.Configuration);

// var app = builder.Build();

// if(app.Environment.IsDevelopment())
// {
//     app.UseDeveloperExceptionPage();
//     app.UseSwagger();
//     app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Clean v1"));
// }

// app.UseHttpsRedirection();

// app.UseAuthentication();

// app.UseAuthorization();

// app.MapControllers();

// try{
//     Log.Information("Configuring Host ....");

//     var host = CreateHostBuilder(args).Build();

//     Log.Information("Applying Migration/s ...");

//     host.MigrateDatabase<CleanContext>((context, services) => {
//         var env = services.GetService<IWebHostEnvironment>();
//         var logger = services.GetService<ILogger<CleanContextSeed>>();
//         CleanContextSeed.SeedAsync(context, logger).Wait();
//     });

//     Log.Information("Host Starting ....");

//     host.Run();
//     // app.Run();
//     return 0;
// }
// catch(Exception ex)
// {
//     Log.Fatal(ex, "Unexpected Error");
//     return 1;
// }
// finally
// {
//     Log.CloseAndFlush();
// }

// IHostBuilder CreateHostBuilder(string[] args) => 
//     Host.CreateDefaultBuilder(args)
//         .ConfigureWebHostDefaults(webBuilder => {
//             webBuilder.UseStartup<Startup>();
//         });

// Serilog.ILogger CreateSerilogLogger(IConfiguration configuration)
// {
//     return new LoggerConfiguration()
//         .MinimumLevel.Verbose()
//         .Enrich.FromLogContext()
//         .WriteTo.Console()
//         .ReadFrom.Configuration(configuration)
//         .CreateLogger();
// }