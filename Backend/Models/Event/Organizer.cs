using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models.Event;

[Index(nameof(Name), IsUnique = true)]
public class Organizer : BaseEntity
{
    public required int OrganizerID {get; set;}

    public required string Name {get; set;}

    public string? Website {get; set;}
}
