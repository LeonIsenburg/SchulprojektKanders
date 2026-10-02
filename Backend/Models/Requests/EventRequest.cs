namespace Backend.Models.Requests;

public class EventRequest
{
    public string? Name {get; set;}

    public string? Type {get; set;}

    public string? Status {get; set;}

    public List<EventDateRequest?>? Dates {get; set;}

    public List<ArtistRequest?>? Artists {get; set;}

    public OrganizerRequest? Organizer {get; set;}

    public int? AgeRestriction {get; set;}

    public string? Description {get; set;}

    public string? Website {get; set;}

    public SocialsRequest? Socials {get; set;}

    public List<string?>? ExtraInfo {get; set;}
}
