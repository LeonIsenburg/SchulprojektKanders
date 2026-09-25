using Backend.Models;
using Backend.Models.Event;
using Backend.Models.Music;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models.Relations;

[Index(nameof(PartyId), nameof(BandId), IsUnique = true)]
public class PartyBand : BaseEntity
{
    public required Guid PartyId {get; set;}
    public Party Party {get; set;} = null!;

    public required Guid BandId {get; set;}
    public Band Band {get; set;} = null!;
}
