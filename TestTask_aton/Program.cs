using TestTask_aton.Core.Abstractions;
using TestTask_aton.Application.Services;
using TestTask_aton.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using TestTask_aton.DataAccess;
using TestTask_aton.Infrastructure;
using Microsoft.AspNetCore.CookiePolicy;
using TestTask_aton.Extentions;
using Microsoft.OpenApi.Writers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
var services = builder.Services;
var configuration = builder.Configuration;

services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
services.AddApiAthentication(configuration.GetSection(nameof(JwtOptions)));
services.AddSwaggerGen();

builder.Services.AddDbContext<UsersDBContext>(
    options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(UsersDBContext)));
    });


services.AddScoped<IUsersService, UsersService>();
services.AddScoped<IUsersRepository, UsersRepository>();
services.AddScoped<IJWTProvider, JWTProvider>();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var repository = scope.ServiceProvider.GetRequiredService<IUsersRepository>();
    await repository.InitializeAdmin();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
