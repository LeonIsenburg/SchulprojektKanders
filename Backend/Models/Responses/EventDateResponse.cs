namespace Backend.Models.Responses;

public class EventDateResponse
{
    public required DateTime Start {get; set;}

    public DateTime? End {get; set;}

    public DateTime? DoorsOpen {get; set;}

    public required LocationResponse Location {get; set;}

    public required TicketsResponse Tickets {get; set;}
}
