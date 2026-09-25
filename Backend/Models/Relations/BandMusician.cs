using Backend.Models;
using Backend.Models.Music;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models.Relations;

[Index(nameof(BandId), nameof(MusicianId), IsUnique = true)]
public class BandMusician : BaseEntity
{
    public required Guid BandId {get; set;}
    public Band Band {get; set;} = null!;

    public required Guid MusicianId {get; set;}
    public Musician Musician {get; set;} = null!;
}
