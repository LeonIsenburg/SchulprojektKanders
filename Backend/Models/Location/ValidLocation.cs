using Backend.Models;

namespace Backend.Models.Location;

public class ValidLocation : BaseEntity
{
    public required int ValidLocationID {get; set;}

    public string? PostalCode {get; set;}

    public string? Street {get; set;}

    public string? HouseNumber {get; set;}

    public string? Country {get; set;}

    public Guid? CityId {get; set;}
    public City? City {get; set;}

    public Guid? DistrictId {get; set;}
    public District? District {get; set;}

    public Guid? RegionId {get; set;}
    public Region? Region {get; set;}

    public Guid? StateId {get; set;}
    public State? State {get; set;}

    public Guid? EventLocationId {get; set;}
    public EventLocation? EventLocation {get; set;}
}
