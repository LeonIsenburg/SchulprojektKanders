using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models.Location;

[Index(nameof(Name), IsUnique = true)]
public class State : BaseEntity
{
    public required int StateID {get; set;}

    public required string Name {get; set;}
}