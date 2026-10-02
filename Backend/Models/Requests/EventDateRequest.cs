namespace Backend.Models.Requests;

public class EventDateRequest
{
    public DateTime? Start {get; set;}

    public DateTime? End {get; set;}

    public DateTime? DoorsOpen {get; set;}

    public LocationRequest? Location {get; set;}

    public TicketsRequest? Tickets {get; set;}
}
