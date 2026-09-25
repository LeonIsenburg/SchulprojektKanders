using Backend.Models;
using Backend.Models.Relations;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models.Music;

[Index(nameof(Description), IsUnique = true)]
public class MusicGenre : BaseEntity
{
    public required int MusicGenreID {get; set;}

    public required string Description {get; set;}

    public ICollection<BandMusicGenre> Bands {get; set;} = new List<BandMusicGenre>();
}