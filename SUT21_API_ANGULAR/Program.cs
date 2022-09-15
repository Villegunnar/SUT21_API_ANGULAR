using Microsoft.EntityFrameworkCore;
using SUT21_API_ANGULAR.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



//Inject DbContext
builder.Services.AddDbContext<CardsDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("UserContextConnection")));

builder.Services.AddCors((setup) =>
{
    setup.AddPolicy("default", (options =>
    {
        options.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
    }));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("default");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
