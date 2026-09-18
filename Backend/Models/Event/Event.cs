using Backend.Models;

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

    /* FK   MITG_ID         INT          NOT NULL,
            GORT_ID         INT          NOT NULL */
}