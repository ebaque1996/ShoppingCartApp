using Microsoft.EntityFrameworkCore;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.Services;
using ShoppingCart.Infraestructure.Persistence.Contexts;
using ShoppingCart.Infraestructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<IProductRepository, SqlProductRepository>();
builder.Services.AddScoped<CartService>();
builder.Services.AddScoped<ICartRepository, SqlCartRepository>();
builder.Services.AddDbContext<ShoppingCartDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

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
