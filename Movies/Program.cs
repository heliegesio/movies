using Microsoft.EntityFrameworkCore;
using Movies.Infrastructure.Data;
using Movies.Infrastructure.Repositories;
using System.Data.Entity;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MoviesDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnectionString")));


builder.Services.AddScoped<SeedDB>();

builder.Services.AddScoped<IProducerRepository, ProducerRepository>();

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(Program)); 
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();



var dbContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<MoviesDbContext>();
dbContext.Database.EnsureDeleted();
dbContext.Database.Migrate();

var seed = app.Services.CreateScope().ServiceProvider.GetRequiredService<SeedDB>();
await seed.Seed();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
