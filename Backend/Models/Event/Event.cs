using Backend.Models;
using Backend.Models.Location;

namespace Backend.Models.Event;

public class Event : BaseEntity
{
    public required int EventID {get; set;}

    public required DateOnly StartDate {get; set;}

    public required TimeOnly StartTime {get; set;}

    public required string StartDay {get; set;}

    public required TimeOnly EntryTime {get; set;}

    public required double Price {get; set;}

    public required PriceType PriceType {get; set;}

    public required EventType EventType {get; set;}

    public required Status Status {get; set;}

    public required Guid MemberId {get; set;}
    public Backend.Models.Member.Member Member {get; set;} = null!;

    public required Guid ValidLocationId {get; set;}
    public ValidLocation ValidLocation {get; set;} = null!;

    public Concert? Concert {get; set;}
    public Festival? Festival {get; set;}
    public Party? Party {get; set;}
}