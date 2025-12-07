using ChemicalMoleculeApi.Data;
using ChemicalMoleculeApi.Services;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")!
        .Replace("{CDDB}", Environment.GetEnvironmentVariable("CDDB") ?? string.Empty)
        .Replace("{CDUSER}", Environment.GetEnvironmentVariable("CDUSER") ?? string.Empty)
        .Replace("{CDPASS}", Environment.GetEnvironmentVariable("CDPASS") ?? string.Empty));
});
builder.Services.AddScoped<IMoleculeService, MoleculeService>();
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
