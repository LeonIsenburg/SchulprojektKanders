namespace Backend.Models.Requests;

public class LocationRequest
{
    public string? Venue {get; set;}

    public string? Street {get; set;}

    public string? HouseNumber {get; set;}

    public string? PostalCode {get; set;}

    public string? City {get; set;}

    public string? District {get; set;}

    public string? Region {get; set;}

    public string? State {get; set;}

    public string? Country {get; set;}
}
