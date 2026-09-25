using Backend.Models.Event;

namespace Backend.Models.Requests;

public class EventRequest
{
    public required DateOnly StartDate {get; set;}

    public required TimeOnly StartTime {get; set;}

    public required TimeOnly EntryTime {get; set;}

    public required double Price {get; set;}

    public required PriceType PriceType {get; set;}

    public required EventType EventType {get; set;}

    public required LocationRequest Location {get; set;}

    public ConcertRequest? Concert {get; set;}

    public FestivalRequest? Festival {get; set;}

    public PartyRequest? Party {get; set;}
}
