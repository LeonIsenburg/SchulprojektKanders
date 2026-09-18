using Backend.Models;

namespace Backend.Models.Location;

public class City : BaseEntity
{
    public required int CityId {get; set;}

    public required string Name {get; set;}
}