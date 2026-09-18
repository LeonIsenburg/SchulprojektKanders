using Backend.Models;

namespace Backend.Models.Music;

public class Band : BaseEntity
{
    public required int BandID {get; set;}

    public required string Name {get; set;}
}