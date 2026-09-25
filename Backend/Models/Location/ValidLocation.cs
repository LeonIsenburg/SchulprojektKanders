using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models.Location;

[Index(nameof(CityId), nameof(DistrictId), nameof(RegionId), nameof(StateId), nameof(EventLocationId), IsUnique = true)]
public class ValidLocation : BaseEntity
{
    public required int ValidLocationID {get; set;}

    public required string PostalCode {get; set;}

    public required string Street {get; set;}

    public required string HouseNumber {get; set;}

    public required Guid CityId {get; set;}
    public City City {get; set;} = null!;

    public required Guid DistrictId {get; set;}
    public District District {get; set;} = null!;

    public required Guid RegionId {get; set;}
    public Region Region {get; set;} = null!;

    public required Guid StateId {get; set;}
    public State State {get; set;} = null!;

    public required Guid EventLocationId {get; set;}
    public EventLocation EventLocation {get; set;} = null!;
}