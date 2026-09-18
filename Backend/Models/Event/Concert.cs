using Backend.Models;

namespace Backend.Models.Event;

public class Concert : BaseEntity
{
    public required int ConcertID {get; set;}

    public required string Organizer {get; set;}
}