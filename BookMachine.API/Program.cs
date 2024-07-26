using BookMachine.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

AuthorServiceExtensions.AddAuthorServiceExtensions(builder.Services);
BookServiceExtensions.AddBookServiceExtensions(builder.Services);
UserServiceExtensions.AddUserServiceExtensions(builder.Services);

DatabaseServiceExtensions.AddDatabaseServiceExtensions(builder.Services, builder.Configuration);

AuthenticationServiceExtensions.AddAuthenticationServiceExtensions(builder.Services, builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCookiePolicy();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();