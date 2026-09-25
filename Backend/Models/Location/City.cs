using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models.Location;

[Index(nameof(Name), IsUnique = true)]
public class City : BaseEntity
{
    public required int CityId {get; set;}

    public required string Name {get; set;}
}