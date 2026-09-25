namespace Backend.Models.Responses;

public class LocationResponse
{
    public required string PostalCode {get; set;}

    public required string Street {get; set;}

    public required string HouseNumber {get; set;}

    public required string City {get; set;}

    public required string District {get; set;}

    public required string Region {get; set;}

    public required string State {get; set;}

    public required string EventLocationName {get; set;}
}
