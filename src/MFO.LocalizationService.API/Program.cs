using MFO.LocalizationService.Application;
using MFO.LocalizationService.Application.Mapping;
using MFO.LocalizationService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly);

    // Add to the pipeline.
    cfg.AddOpenBehavior(typeof(ValidationMiddleware<,>));
});

builder.Services.AddAutoMapper(cfg => cfg.AddProfile(new LocalizationServiceProfile()));

builder.Services.AddDbContext<LocalizationContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("LocalizationContext")));
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