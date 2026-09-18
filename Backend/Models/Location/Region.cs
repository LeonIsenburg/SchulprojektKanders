using Backend.Models;

namespace Backend.Models.Location;

public class Region : BaseEntity
{
    public required int RegionID {get; set;}

    public required string Name {get; set;}
}