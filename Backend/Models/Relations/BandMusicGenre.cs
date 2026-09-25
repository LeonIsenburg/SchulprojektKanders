using Backend.Models;
using Backend.Models.Music;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models.Relations;

[Index(nameof(BandId), nameof(MusicGenreId), IsUnique = true)]
public class BandMusicGenre : BaseEntity
{
    public required Guid BandId {get; set;}
    public Band Band {get; set;} = null!;

    public required Guid MusicGenreId {get; set;}
    public MusicGenre MusicGenre {get; set;} = null!;
}
