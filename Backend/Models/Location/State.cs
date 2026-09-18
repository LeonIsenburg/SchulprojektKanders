using Backend.Models;

namespace Backend.Models.Location;

public class State : BaseEntity
{
    public required int StateID {get; set;}

    public required string Name {get; set;}
}