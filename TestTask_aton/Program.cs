using TestTask_aton.Core.Abstractions;
using TestTask_aton.Application.Services;
using TestTask_aton.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using TestTask_aton.DataAccess;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<UsersDBContext>(
    options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(UsersDBContext)));
    });

builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
