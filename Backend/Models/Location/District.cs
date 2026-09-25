using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models.Location;

[Index(nameof(Name), IsUnique = true)]
public class District : BaseEntity
{
    public required int DistrictID {get; set;}

    public required string Name {get; set;}
}