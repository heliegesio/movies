using Microsoft.EntityFrameworkCore;
using Movies.Infrastructure.Data;
using Movies.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Registra o repositório do produtor
builder.Services.AddScoped<IProducerRepository, ProducerRepository>();


// Registre a classe SeedDB
builder.Services.AddScoped<SeedDB>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("SqliteConnectionString");
builder.Services.AddDbContext<MoviesDbContext>(options =>
    options.UseSqlite(connectionString)
);


var app = builder.Build();


var dbContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<MoviesDbContext>();
//dbContext.Database.EnsureDeleted();
dbContext.Database.Migrate();

var seed = app.Services.CreateScope().ServiceProvider.GetRequiredService<SeedDB>();
await seed.Seed();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
