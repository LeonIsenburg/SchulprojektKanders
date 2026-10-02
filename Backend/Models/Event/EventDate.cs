using Backend.Models;
using Backend.Models.Location;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models.Event;

[Index(nameof(EventId), nameof(Position), IsUnique = true)]
public class EventDate : BaseEntity
{
    public required Guid EventId {get; set;}
    public Event Event {get; set;} = null!;

    public int Position {get; set;}

    public required DateTime Start {get; set;}

    public DateTime? End {get; set;}

    public DateTime? DoorsOpen {get; set;}

    public Guid? ValidLocationId {get; set;}
    public ValidLocation? ValidLocation {get; set;}

    public double? Price {get; set;}

    public string? Currency {get; set;}

    public bool? Presale {get; set;}

    public string? TicketUrl {get; set;}
}
