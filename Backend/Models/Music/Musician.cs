using Backend.Models;
using Backend.Models.Relations;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models.Music;

[Index(nameof(ArtistName), IsUnique = true)]
public class Musician : BaseEntity
{
    public required int MusicianID {get; set;}

    public required string ArtistName {get; set;}

    public string? FirstName {get; set;}

    public string? LastName {get; set;}

    public ICollection<MusicianInstrument> Instruments {get; set;} = new List<MusicianInstrument>();

    public ICollection<BandMusician> Bands {get; set;} = new List<BandMusician>();
}