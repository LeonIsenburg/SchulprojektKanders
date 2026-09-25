using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models.Event;

[Index(nameof(Name), IsUnique = true)]
public class FestivalName : BaseEntity
{
    public required int Festival_NameID {get; set;}

    public required string Name {get; set;}
}