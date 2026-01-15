using GameStore.api.Dtos;
using GameStore.api.Entities; 

namespace GameStore.api.Mapping;

public static class GameMapping
{
    public static Game ToEntity(this CreateGameDto dto)
    {
        return new Game
        {
            Name = dto.Name,
            GenreId = dto.GenreId,
            Price = dto.Price,
            ReleaseDate = dto.ReleaseDate
        };
    }

    public static GameDto ToDto(this Game game)     
    {
        return new GameDto(
            game.Id,
            game.Name,
            game.Genre?.Name ?? "Unknown",
            game.Price,
            game.ReleaseDate
        );
    }
}