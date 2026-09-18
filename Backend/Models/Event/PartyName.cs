using Backend.Models;

namespace Backend.Models.Event;

public class PartyName : BaseEntity
{
    public required int Party_NameID {get; set;}

    public required string Name {get; set;}
}