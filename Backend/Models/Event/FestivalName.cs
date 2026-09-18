using Backend.Models;

namespace Backend.Models.Event;

public class FestivalName : BaseEntity
{
    public required int Festival_NameID {get; set;}

    public required string Name {get; set;}
}