using GameStore.api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

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

app.MapGet("games", () => games);

app.MapGet("/", () => "Hello World!");

app.Run();
