using Backend.Models;
using Backend.Models.Relations;

namespace Backend.Models.Event;

public class Event : BaseEntity
{
    public required int EventID {get; set;}

    public required string Name {get; set;}

    public required EventType Type {get; set;}

    public required Status Status {get; set;}

    public int? AgeRestriction {get; set;}

    public string? Description {get; set;}

    public string? Website {get; set;}

    public string? Instagram {get; set;}

    public string? Facebook {get; set;}

    public string? TikTok {get; set;}

    public List<string> ExtraInfo {get; set;} = [];

    public Guid? OrganizerId {get; set;}
    public Organizer? Organizer {get; set;}

    public required Guid MemberId {get; set;}
    public Backend.Models.Member.Member Member {get; set;} = null!;

    public ICollection<EventDate> Dates {get; set;} = new List<EventDate>();

    public ICollection<EventArtist> Artists {get; set;} = new List<EventArtist>();
}
