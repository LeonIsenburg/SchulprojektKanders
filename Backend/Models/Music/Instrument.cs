using Backend.Models;

namespace Backend.Models.Music;

public class Instrument : BaseEntity
{
    public required int InstrumentID {get; set;}

    public required string Name {get; set;}
}