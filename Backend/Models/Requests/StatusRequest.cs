using Backend.Models.Event;

namespace Backend.Models.Requests;

public class StatusRequest
{
    public required Status Status {get; set;}
}
