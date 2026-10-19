using challengeFiap.Application.Service;
using challengeFiap.Domain.Interfaces;
using challengeFiap.Infrastruture;
using challengeFiap.Infrastruture.Data;
using ChallengeFiap.Infrastruture.Data;
using ChallengeFiap.Infrastruture.Repositorie.MongoRepository;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Serilog;
using Serilog.Events;
using System.Security.Cryptography.X509Certificates;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {SourceContext} - {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

try
{
    Log.Information("Starting web host");

    var builder = WebApplication.CreateBuilder(args);

    // Integrar Serilog com o host
    builder.Host.UseSerilog();

    //Parte de Banco de dados

    //mongodb
    builder.Services.Configure<MongoStoreDataBaseSettings>(
        builder.Configuration.GetSection("MongoStoreDataBaseSettings"));

        //Cliente do mongo
        builder.Services.AddSingleton<IMongoClient>(sp =>
        {
            var settings = sp
            .GetRequiredService<IOptions<MongoStoreDataBaseSettings>>()
            .Value;

            var mongoSettings = MongoClientSettings
            .FromConnectionString(settings.ConnectionString);

            mongoSettings.ServerApi = new ServerApi(ServerApiVersion.V1);

            return new MongoClient(mongoSettings);
        });
    //Mongo Database Context
    builder.Services.AddSingleton<IMongoDatabase>(sp => 
    {
            var settings = sp
            .GetRequiredService<IOptions<MongoStoreDataBaseSettings>>()
            .Value;

            var client = sp.GetRequiredService<IMongoClient>();

            return client.GetDatabase(settings.DatabaseName);
    });

    builder.Services.AddSingleton<MongoDbContext>();

    //oracle

    var connectionString = builder.Configuration.GetConnectionString("OracleConnection");

    builder.Services.AddDbContext<AppDbContext>(
       options =>
       options.UseOracle(connectionString));

    //Serviços externos

    //Parte de HealthCheck

    builder.Services.AddHealthChecks()
        .AddCheck("self", () => Microsoft
        .Extensions
        .Diagnostics
        .HealthChecks.HealthCheckResult.Healthy(), tags: new[] { "live" })
        .AddDbContextCheck<AppDbContext>("oracle", tags: new[] { "ready" })
        .AddUrlGroup(
        new Uri("https://jsonplaceholder.typicode.com/posts/1"),
        name: "jsonplaceholder",
        tags: new[] { "ready" });

   //services
   builder.Services.AddInfrastructure();

    //Parte de Controllers E Authorization

    builder.Services.AddControllers();
    builder.Services.AddAuthorization();

    //Parte de Swagger

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddOpenApi();
    builder.Services.AddSwaggerGen(options =>
    {
        var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
        if (File.Exists(xmlPath))
        {
            options.IncludeXmlComments(xmlPath);
        }
    }
    );

    //Parte de Serialização

    var app = builder.Build();

    // Configure the HTTP request pipeline.

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Challenge CYLVO");
            options.RoutePrefix = "swagger";
        });
    }


    app.UseHttpsRedirection();

    // Request logging estruturado com Serilog
    app.UseSerilogRequestLogging();

    app.MapHealthChecks("/health/live", new HealthCheckOptions
    {
        Predicate = check => check.Tags.Contains("live")
    });

    app.MapHealthChecks("/health/ready", new HealthCheckOptions
    {
        Predicate = check => check.Tags.Contains("ready"),
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

    app.MapControllers();
    app.Run();

}

catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

public partial class Program { }

