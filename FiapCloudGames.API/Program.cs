using FiapCloudGames.API.Endpoints;
using FiapCloudGames.Application.Sevices;
using FiapCloudGames.Core.Repositories;
using FiapCloudGames.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Registrando o ApplicationDbContext
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//{
//    options.UseSql builder.Configuration.GetConnectionString("SQLServer");

//    options.UseMongoDB(connectionString, databaseName);
//});

#region [DI]
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IGameRepository, GameRepository>();

builder.Services.AddScoped<UserService>();
#endregion

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGameEndpoints();
app.MapUserEndpoints();

app.Run();