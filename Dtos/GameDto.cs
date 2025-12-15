using System;

namespace GameStore.api.Dtos;

public record class GameDto(
    int Id, 
    string Name, 
    string Genre, 
    decimal price, 
    DateOnly ReleaseDate);
