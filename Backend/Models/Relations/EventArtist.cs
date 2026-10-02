using Backend.Models;
using Backend.Models.Music;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models.Relations;

[Index(nameof(EventId), nameof(BandId), IsUnique = true)]
public class EventArtist : BaseEntity
{
    public required Guid EventId {get; set;}
    public Backend.Models.Event.Event Event {get; set;} = null!;

    public required Guid BandId {get; set;}
    public Band Band {get; set;} = null!;

    public int Position {get; set;}

    public DateTime? Performance {get; set;}
}
