using Backend.Models;

namespace Backend.Models.Location;

public class District : BaseEntity
{
    public required int DistrictID {get; set;}

    public required string Name {get; set;}
}