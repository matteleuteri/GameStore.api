using GameStore.api.Dtos;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

const string GetGameEndpointName = "GetGame";

List<GameDto> games = [
    new(
        1, 
        "Pokemon: Heartgold",
        "Role-Playing",
        12.72M,
        new DateOnly(2010, 3, 14)),
    new (
        2, 
        "Minecraft", 
        "Sandbox",
        350M, 
        new DateOnly(2009, 5, 17)),
    new (
        3, 
        "Wii Sports Resort",
        "Sports", 
        33.14M ,
        new DateOnly(2009, 6, 25))
];

// GET /games
app.MapGet("games", () => games);

app.MapGet("/", () => "Hello World!");

// GET /games/1
app.MapGet("games/{id}", (int id) => games.Find( game => game.Id == id)).WithName(GetGameEndpointName);

app.MapPost("games", (CreateGameDto newGame) => 
{
    GameDto game = new(
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate);
    
    games.Add(game);
    return Results.CreatedAtRoute(GetGameEndpointName, new {id = game.Id}, game);
});

// PUT /games/1
app.MapPut("games/{id}", (int id, UpdateGameDto updatedGame) => 
{
    var index = games.FindIndex(game => game.Id == id);

    if (index == -1)
    {
        return Results.NotFound();
    }

    games[index] = new GameDto(
        id,
        updatedGame.Name,
        updatedGame.Genre,
        updatedGame.Price,
        updatedGame.ReleaseDate
    );

    return Results.NoContent();
});

// DELETE /games/1
app.MapDelete("games/{id}", (int id)=>
{
    games.RemoveAll(games => games.Id == id);

    return Results.NoContent();
});
app.Run();

// POST /games
// app.MapPost
