using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models.Location;

[Index(nameof(Name), IsUnique = true)]
public class EventLocation : BaseEntity
{
    public required int Event_LocationID {get; set;}

    public required string Name {get; set;}
}