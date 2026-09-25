using Backend.Models;
using Backend.Models.Relations;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models.Music;

[Index(nameof(ArtistName), IsUnique = true)]
[Index(nameof(FirstName), nameof(LastName), IsUnique = true)]
public class Musician : BaseEntity
{
    public required int MusicianID {get; set;}

    public required string ArtistName {get; set;}

    public required string FirstName {get; set;}

    public required string LastName {get; set;}

    public ICollection<MusicianInstrument> Instruments {get; set;} = new List<MusicianInstrument>();

    public ICollection<BandMusician> Bands {get; set;} = new List<BandMusician>();
}