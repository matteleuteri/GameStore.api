using GameStore.api.Endpoints;
using GameStore.Api.Data;

var builder = WebApplication.CreateBuilder(args);

var connString = "Data Source=GameStore.db";
builder.Services.AddSqlite<GameStoreContext>(connString);

var app = builder.Build();

app.MapGamesEndpoints();

app.MigrateDB();

app.Run();

// POST /games
// app.MapPost
