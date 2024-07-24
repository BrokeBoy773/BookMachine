using BookMachine.Application.Services;
using BookMachine.Core.Interfaces.Application.Services;
using BookMachine.Core.Interfaces.Persistence.Repositories;
using BookMachine.Persistence;
using BookMachine.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IBookRepository, BookRepository>();
builder.Services.AddTransient<IBookService, BookService>();

builder.Services.AddTransient<IAuthorRepository, AuthorRepository>();
builder.Services.AddTransient<IAuthorService, AuthorService>();

builder.Services.AddDbContext<BookMachineDbContext>(
    options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(BookMachineDbContext)));
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();