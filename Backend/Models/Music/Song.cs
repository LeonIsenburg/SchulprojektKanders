using Backend.Models;

namespace Backend.Models.Music;

public class Song : BaseEntity
{
    public required int SongID {get; set;}

    public required string Name {get; set;}
}