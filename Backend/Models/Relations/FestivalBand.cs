using Backend.Models;
using Backend.Models.Event;
using Backend.Models.Music;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models.Relations;

[Index(nameof(FestivalId), nameof(BandId), IsUnique = true)]
public class FestivalBand : BaseEntity
{
    public required Guid FestivalId {get; set;}
    public Festival Festival {get; set;} = null!;

    public required Guid BandId {get; set;}
    public Band Band {get; set;} = null!;

    public DateOnly? Date {get; set;}

    public TimeOnly? Time {get; set;}

    public string? Day {get; set;}
}
