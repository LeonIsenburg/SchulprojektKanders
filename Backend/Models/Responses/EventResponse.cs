using Backend.Models.Event;

namespace Backend.Models.Responses;

// Guid, Status und StartDay bleiben bewusst intern und werden nicht ausgegeben
public class EventResponse
{
    public required DateOnly StartDate {get; set;}

    public required TimeOnly StartTime {get; set;}

    public required TimeOnly EntryTime {get; set;}

    public required double Price {get; set;}

    public required PriceType PriceType {get; set;}

    public required EventType EventType {get; set;}

    public required LocationResponse Location {get; set;}

    public ConcertResponse? Concert {get; set;}

    public PartyResponse? Party {get; set;}

    public FestivalResponse? Festival {get; set;}
}
