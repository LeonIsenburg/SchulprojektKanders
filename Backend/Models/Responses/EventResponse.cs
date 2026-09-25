using Backend.Models.Event;

namespace Backend.Models.Responses;

public class EventResponse
{
    public required Guid Guid {get; set;}

    public required DateOnly StartDate {get; set;}

    public required TimeOnly StartTime {get; set;}

    public required string StartDay {get; set;}

    public required TimeOnly EntryTime {get; set;}

    public required double Price {get; set;}

    public required PriceType PriceType {get; set;}

    public required EventType EventType {get; set;}

    public required Status Status {get; set;}

    public required LocationResponse Location {get; set;}

    public ConcertResponse? Concert {get; set;}

    public FestivalResponse? Festival {get; set;}

    public PartyResponse? Party {get; set;}
}
