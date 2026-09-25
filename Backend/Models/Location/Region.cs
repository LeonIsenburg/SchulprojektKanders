using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models.Location;

[Index(nameof(Name), IsUnique = true)]
public class Region : BaseEntity
{
    public required int RegionID {get; set;}

    public required string Name {get; set;}
}