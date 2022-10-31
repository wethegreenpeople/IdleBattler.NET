using IdleBattler_Server.Arena.Services;
using IdleBattler_Server.Arena.Stores;
using IdleBattler_Server.Fighter.Stores;
using IdleBattler_Server.Jobs;
using Quartz;
using System.Collections.Specialized;

var builder = WebApplication.CreateBuilder(args);
var _corsOrigins = "_corsOrigins";



// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IArenaStore, ArenaInMemoryStore>();
builder.Services.AddScoped<IMovementService, MovementService>();
builder.Services.AddScoped<IMovementStore, MovementStore>();
builder.Services.AddScoped<IFighterStore, InMemoryFighterStore>();
builder.Services.AddScoped<ITreasureService, TreasureService>();
builder.Services.AddScoped<ITreasureStore, TreasureStore>();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: _corsOrigins, builder =>
    {
        builder.WithOrigins("http://localhost:7177", "https://localhost:7177");
    });
});

builder.Services.AddQuartz(q =>
{
    // base quartz scheduler, job and trigger configuration
    q.UseMicrosoftDependencyInjectionScopedJobFactory();

    var arenaFighterJob = new JobKey("AddArenaFighterJob");
    q.AddJob<AddFightersJob>(opts => opts.WithIdentity(arenaFighterJob));
    q.AddTrigger(opt => opt
        .ForJob(arenaFighterJob)
        .WithIdentity("AddFighterJob-Trigger")
        .WithCronSchedule("0/10 * * * * ?"));
});

// ASP.NET Core hosting
builder.Services.AddQuartzServer(options =>
{
    // when shutting down we want jobs to complete gracefully
    options.WaitForJobsToComplete = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(_corsOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
