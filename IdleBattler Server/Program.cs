using IdleBattler_Server.Arena.Services;
using IdleBattler_Server.Arena.Stores;
using IdleBattler_Server.Fighter.Stores;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IArenaStore, ArenaInMemoryStore>();
builder.Services.AddScoped<IMovementService, MovementService>();
builder.Services.AddScoped<IMovementStore, MovementStore>();
builder.Services.AddScoped<IFighterStore, InMemoryFighterStore>();

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
