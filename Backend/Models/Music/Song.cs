using Backend.Models;
using Backend.Models.Relations;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models.Music;

[Index(nameof(Name), IsUnique = true)]
public class Song : BaseEntity
{
    public required int SongID {get; set;}

    public required string Name {get; set;}

    public ICollection<BandSong> Bands {get; set;} = new List<BandSong>();
}