using Backend.Models;
using Backend.Models.Music;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models.Relations;

[Index(nameof(BandId), nameof(SongId), IsUnique = true)]
public class BandSong : BaseEntity
{
    public required Guid BandId {get; set;}
    public Band Band {get; set;} = null!;

    public required Guid SongId {get; set;}
    public Song Song {get; set;} = null!;
}
