using IdleBattler_Server.Arena.Services;
using IdleBattler_Server.Arena.Stores;
using IdleBattler_Server.Fighter.Stores;
using IdleBattler_Server.Hubs;
using IdleBattler_Server.Jobs;
using Microsoft.AspNetCore.ResponseCompression;
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

builder.Services.AddQuartz(q =>
{
    // base quartz scheduler, job and trigger configuration
    q.UseMicrosoftDependencyInjectionJobFactory();

    var arenaFighterJob = new JobKey("AddArenaFighterJob");
    q.AddJob<AddFightersJob>(opts => opts.WithIdentity(arenaFighterJob));
    q.AddTrigger(opt => opt
        .ForJob(arenaFighterJob)
        .WithIdentity($"{arenaFighterJob}-Trigger")
        .WithCronSchedule("0/10 * * * * ?"));

    var arenaCreationJob = new JobKey("CreateArenaJob");
    q.AddJob<AddArenasJob>(opts => opts.WithIdentity(arenaCreationJob));
    q.AddTrigger(opt => opt
        .ForJob(arenaCreationJob)
        .WithIdentity($"{arenaCreationJob}-Trigger")
        .StartNow()
        .WithCronSchedule("0/30 * * * * ?"));
});

// ASP.NET Core hosting
builder.Services.AddQuartzServer(options =>
{
    // when shutting down we want jobs to complete gracefully
    options.WaitForJobsToComplete = true;
});

builder.Services.AddSignalR();
builder.Services.AddResponseCompression(opt =>
{
    opt.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});

var app = builder.Build();

app.UseResponseCompression();

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

app.UseCors(builder =>
{
    builder.WithOrigins("http://localhost:7177", "https://localhost:7177")
        .AllowAnyHeader()
        .WithMethods("GET", "POST")
        .AllowCredentials();
});

app.MapHub<ArenaHub>("/arenahub");

app.Run();
