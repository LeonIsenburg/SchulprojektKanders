using Backend.Models.Event;

namespace Backend.Models.Responses;

public class EventResponse
{
    public required string Name {get; set;}

    public required EventType Type {get; set;}

    public required Status Status {get; set;}

    public required List<EventDateResponse> Dates {get; set;}

    public required List<ArtistResponse> Artists {get; set;}

    public required OrganizerResponse Organizer {get; set;}

    public int? AgeRestriction {get; set;}

    public string? Description {get; set;}

    public string? Website {get; set;}

    public required SocialsResponse Socials {get; set;}

    public required List<string> ExtraInfo {get; set;}
}
