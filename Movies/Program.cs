using Microsoft.EntityFrameworkCore;
using Movies.Infrastructure.Data;
using Movies.Infrastructure.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços como antes
builder.Services.AddDbContext<MoviesDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnectionString")));



// Registre seu repositório
builder.Services.AddScoped<IProducerRepository, ProducerRepository>();

builder.Services.AddControllers();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

// Registre a classe SeedDB
//builder.Services.AddScoped<SeedDB>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();


var options = new DbContextOptionsBuilder<MoviesDbContext>()
        .UseSqlite("Data Source=DbMovies.db")
        .Options;

using (var context = new MoviesDbContext(options))
{
    context.Database.EnsureCreated(); // Isso garantirá que o banco de dados seja criado
}

Console.WriteLine("Database created successfully.");

var dbContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<MoviesDbContext>();
dbContext.Database.EnsureDeleted();
dbContext.Database.EnsureCreated();
dbContext.Database.Migrate();

//var seed = app.Services.CreateScope().ServiceProvider.GetRequiredService<SeedDB>();
//await seed.Seed();

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
