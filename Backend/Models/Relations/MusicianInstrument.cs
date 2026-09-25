using Backend.Models;
using Backend.Models.Music;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models.Relations;

[Index(nameof(MusicianId), nameof(InstrumentId), IsUnique = true)]
public class MusicianInstrument : BaseEntity
{
    public required Guid MusicianId {get; set;}
    public Musician Musician {get; set;} = null!;

    public required Guid InstrumentId {get; set;}
    public Instrument Instrument {get; set;} = null!;
}
