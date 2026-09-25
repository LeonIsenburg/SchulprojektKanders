using Backend.Models;
using Backend.Models.Relations;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models.Music;

[Index(nameof(Name), IsUnique = true)]
public class Instrument : BaseEntity
{
    public required int InstrumentID {get; set;}

    public required string Name {get; set;}

    public ICollection<MusicianInstrument> Musicians {get; set;} = new List<MusicianInstrument>();
}