using Backend.Models;
using Backend.Models.Event;
using Backend.Models.Music;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models.Relations;

[Index(nameof(ConcertId), nameof(BandId), IsUnique = true)]
public class ConcertBand : BaseEntity
{
    public required Guid ConcertId {get; set;}
    public Concert Concert {get; set;} = null!;

    public required Guid BandId {get; set;}
    public Band Band {get; set;} = null!;
}
