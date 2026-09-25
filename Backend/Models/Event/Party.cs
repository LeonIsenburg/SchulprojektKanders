using Backend.Models;
using Backend.Models.Relations;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models.Event;

[Index(nameof(EventId), IsUnique = true)]
[Index(nameof(PartyNameId), nameof(EndDate), IsUnique = true)]
public class Party : BaseEntity
{
    public required int PartyID {get; set;}

    public required DateOnly EndDate {get; set;}

    public required TimeOnly EndTime {get; set;}

    public required string EndDay {get; set;}

    public required string Organizer {get; set;}

    public required Guid EventId {get; set;}
    public Event Event {get; set;} = null!;

    public required Guid PartyNameId {get; set;}
    public PartyName PartyName {get; set;} = null!;

    public ICollection<PartyBand> Bands {get; set;} = new List<PartyBand>();
}
