using Microsoft.EntityFrameworkCore;
using probKol2.Data;
using probKol2.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add controllers functionality
builder.Services.AddControllers();
// Dependency injection for AnimalsRepository
builder.Services.AddScoped<IOrderPastryRepository,OrderPastryRepository>();
builder.Services.AddDbContext<DataConcept>(
    options => options.UseSqlServer("Name=ConnectionStrings:Default"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

