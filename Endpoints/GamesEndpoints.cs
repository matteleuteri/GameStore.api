using GameStore.api.Dtos;
using GameStore.api.Entities;
using GameStore.Api.Data;
using GameStore.api.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.api.Endpoints;

public static class GameEndPoints
{
    const string GetGameEndpointName = "GetGame";

    private static readonly List<GameSummaryDto> games = [
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

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games").WithParameterValidation();

        // GET /games
        group.MapGet("/", (GameStoreContext dbContext) => dbContext.Games
            .Include(game => game.Genre)
            .Select(game => game.ToGameSummaryDto())
            .AsNoTracking()
        );

        // GET /games/1
        group.MapGet("/{id}", (int id, GameStoreContext dbContext) => 
        {
            Game? game = dbContext.Games.Find(id);

            return game is null ? Results.NotFound() : Results.Ok(game.ToGameDetailsDto());
        }).WithName(GetGameEndpointName);

        group.MapPost("/", (CreateGameDto newGame, GameStoreContext dbContext) => 
        {
            if (string.IsNullOrEmpty(newGame.Name))
            {
                return Results.BadRequest("Name is required");
            }

            Game game = newGame.ToEntity();   
            game.Genre = dbContext.Genres.Find(newGame.GenreId);     

            dbContext.Games.Add(game);
            dbContext.SaveChanges();

            return Results.CreatedAtRoute(
                GetGameEndpointName, 
                new {id = game.Id}, 
                game.ToGameDetailsDto());
        });

        // PUT /games/1
        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame, GameStoreContext dbContext) => 
        {
            var existingGame = dbContext.Games.Find(id);
            var index = games.FindIndex(game => game.Id == id);

            if (existingGame is null)   
            {
                return Results.NotFound();
            }

            dbContext.Entry(existingGame).CurrentValues.SetValues(updatedGame.ToEntity(id));
            dbContext.SaveChanges();

            return Results.NoContent();
        });

        // DELETE /games/1
        group.MapDelete("/{id}", (int id, GameStoreContext dbContext)=>
        {
            dbContext.Games.Where (g => g.Id == id).ExecuteDelete();

            return Results.NoContent();
        });
        
        return group;
    }
}