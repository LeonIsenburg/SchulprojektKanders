using Backend.Models;
using Backend.Models.Relations;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models.Music;

[Index(nameof(Name), IsUnique = true)]
public class Band : BaseEntity
{
    public required int BandID {get; set;}

    public required string Name {get; set;}

    public ICollection<BandMusician> Musicians {get; set;} = new List<BandMusician>();

    public ICollection<BandSong> Songs {get; set;} = new List<BandSong>();

    public ICollection<BandMusicGenre> MusicGenres {get; set;} = new List<BandMusicGenre>();

    public ICollection<ConcertBand> Concerts {get; set;} = new List<ConcertBand>();

    public ICollection<FestivalBand> Festivals {get; set;} = new List<FestivalBand>();

    public ICollection<PartyBand> Parties {get; set;} = new List<PartyBand>();
}