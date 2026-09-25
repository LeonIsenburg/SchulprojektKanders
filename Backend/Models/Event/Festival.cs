using Backend.Models;
using Backend.Models.Relations;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models.Event;

[Index(nameof(EventId), IsUnique = true)]
[Index(nameof(FestivalNameId), nameof(EndDate), IsUnique = true)]
public class Festival : BaseEntity
{
    public required int FestivalID {get; set;}

    public required DateOnly EndDate {get; set;}

    public required TimeOnly EndTime {get; set;}

    public required string EndDay {get; set;}

    public required string Organizer {get; set;}

    public required Guid EventId {get; set;}
    public Event Event {get; set;} = null!;

    public required Guid FestivalNameId {get; set;}
    public FestivalName FestivalName {get; set;} = null!;

    public ICollection<FestivalBand> Bands {get; set;} = new List<FestivalBand>();
}
