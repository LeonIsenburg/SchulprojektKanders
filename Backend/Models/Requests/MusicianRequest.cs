namespace Backend.Models.Requests;

public class MusicianRequest
{
    public required string ArtistName {get; set;}

    public string? FirstName {get; set;}

    public string? LastName {get; set;}
}
