using Elastic.Channels;
using Elastic.Ingest.Elasticsearch;
using Elastic.Ingest.Elasticsearch.DataStreams;
using Elastic.Serilog.Sinks;
using MFO.LocalizationService.API.Middlewares;
using MFO.LocalizationService.Application;
using MFO.LocalizationService.Application.Mapping;
using MFO.LocalizationService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using NSwag;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddOpenApiDocument(options =>
{
    options.PostProcess = document =>
    {
        document.Info = new OpenApiInfo
        {
            Version = "v1",
            Title = "Localization Service API",
            Description = "An ASP.NET Core Web API for managing Countries, Regions, Currencies, ExchangeRates etc.",
            //TermsOfService = "https://example.com/terms",
            Contact = new OpenApiContact
            {
                Name = "Mihai Negrisan",
                Url = "https://github.com/mihainegrisan/MFO.LocalizationService"
            },
            //License = new OpenApiLicense
            //{
            //    Name = "Example License",
            //    Url = "https://example.com/license"
            //}
        };
    };
});

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly);

    // Add to the pipeline.
    cfg.AddOpenBehavior(typeof(ValidationMiddleware<,>));
});

builder.Services.AddAutoMapper(cfg => cfg.AddProfile(new LocalizationServiceProfile()));

builder.Services.AddControllers();

builder.Services.AddDbContext<LocalizationContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("LocalizationContext")));

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .MinimumLevel.Information()
    .Enrich.FromLogContext()
    .Enrich.WithEnvironmentName()
    .Enrich.WithMachineName()
    .Enrich.WithProcessId()
    .Enrich.WithThreadId()
    .Enrich.WithProperty("Service", "MFO.LocalizationService")
    .WriteTo.Console()
    //.WriteTo.File("logs/localizationservice-.log", rollingInterval: RollingInterval.Day)
    .WriteTo.Elasticsearch(
        [new Uri("http://localhost:9200")],        // ES endpoint(s)
        opts =>
        {
            // Use a data stream (recommended). Structure: type, dataset, namespace.
            // This will target a datastream like: logs-localizationservice-dev
            opts.DataStream = new DataStreamName("logs", "localizationservice", "dev");

            // How the sink should attempt bootstrap templates: None / Silent / Failure
            // Silent = try but don't fail app if templates can't be installed.
            opts.BootstrapMethod = BootstrapMethod.Silent;

            // Optional: tune the in-memory channel (backpressure/batching). Keep defaults unless you need to tweak.
            opts.ConfigureChannel = channelOptions =>
            {
                // set an empty BufferOptions (don't try to use properties that may have been removed)
                channelOptions.BufferOptions = new BufferOptions();
            };
        },
        transport =>
        {
            // if your ES has auth, configure transport here:
            // transport.Authentication(new BasicAuthentication("user","pass"));
        })
    .CreateLogger();

builder.Host.UseSerilog();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

builder.Services.AddHybridCache(options =>
{
    options.MaximumPayloadBytes = 1024 * 1024;
    options.MaximumKeyLength = 1024;
    options.DefaultEntryOptions = new HybridCacheEntryOptions
    {
        Expiration = TimeSpan.FromSeconds(20),
        LocalCacheExpiration = TimeSpan.FromSeconds(20)
    };
});

await app.RunAsync();