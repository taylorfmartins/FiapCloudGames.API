using FiapCloudGames.API.Endpoints.Auth;
using FiapCloudGames.API.Endpoints.Game;
using FiapCloudGames.API.Endpoints.User;
using FiapCloudGames.API.Extensions;
using FiapCloudGames.API.Middlewares;
using FiapCloudGames.Application.Sevices;
using FiapCloudGames.Core.Repositories;
using FiapCloudGames.Core.Services;
using FiapCloudGames.Infrastructure;
using FiapCloudGames.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configuração do Serilog
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

// Registrando o ApplicationDbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServer"));
}, ServiceLifetime.Scoped);

#region [DI]
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IGameRepository, GameRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<TokenService>();

builder.Services.AddScoped<IPasswordHashingService, PasswordHashingService>();
#endregion

// Configuração do Swagger
builder.Services.AddSwaggerConfiguration();

// Configuração do JWT
builder.Services.AddJwtConfiguration(builder.Configuration);

var app = builder.Build();

#region [Middlewares]
app.UseMiddleware<ErrorHandlingMiddleware>();
#endregion

// Configuração do Swagger UI
app.UseSwaggerConfiguration();

// Configuração do JWT
app.UseJwtConfiguration();

app.UseHttpsRedirection();

app.MapGame();
app.MapUser();
app.MapAuth();

app.Run();