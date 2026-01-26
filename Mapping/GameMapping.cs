using GameStore.api.Dtos;
using GameStore.api.Entities; 

namespace GameStore.api.Mapping;

public static class GameMapping
{
    public static Game ToEntity(this CreateGameDto gameDto)
    {
        return new Game()
        {
            Name = gameDto.Name,
            GenreId = gameDto.GenreId,
            Price = gameDto.Price,
            ReleaseDate = gameDto.ReleaseDate
        };
    }

    public static Game ToEntity(this UpdateGameDto game, int id)
    {
        return new Game()
        {
            Id = id,
            Name = game.Name,
            GenreId = game.GenreId,
            Price = game.Price,
            ReleaseDate = game.ReleaseDate
        };
    }

    public static GameSummaryDto ToGameSummaryDto(this Game game)     
    {
        return new (
            game.Id,
            game.Name,
            game.Genre?.Name ?? "Unknown",
            game.Price,
            game.ReleaseDate
        );
    }

    public static GameDetailsDto ToGameDetailsDto(this Game game)     
    {
        return new (
            game.Id,
            game.Name,
            game.GenreId,
            game.Price,
            game.ReleaseDate
        );
    }
}       