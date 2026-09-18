using Backend.Models;

namespace Backend.Models.Music;

public class MusicGenre : BaseEntity
{
    public required int MusicGenreID {get; set;}

    public required string Description {get; set;}
}