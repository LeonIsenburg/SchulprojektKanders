using Backend.Models;
using Backend.Models.Music;
using Backend.Models.Relations;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models.Event;

[Index(nameof(EventId), IsUnique = true)]
public class Concert : BaseEntity
{
    public required int ConcertID {get; set;}

    public required string Organizer {get; set;}

    public required Guid EventId {get; set;}
    public Event Event {get; set;} = null!;

    public required Guid BandId {get; set;}
    public Band Band {get; set;} = null!;

    public ICollection<ConcertBand> Bands {get; set;} = new List<ConcertBand>();
}