using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models.Event;

[Index(nameof(Name), IsUnique = true)]
public class PartyName : BaseEntity
{
    public required int Party_NameID {get; set;}

    public required string Name {get; set;}
}