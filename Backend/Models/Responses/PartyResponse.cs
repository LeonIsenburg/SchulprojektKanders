namespace Backend.Models.Responses;

public class PartyResponse
{
    public required DateOnly EndDate {get; set;}

    public required TimeOnly EndTime {get; set;}

    public required string EndDay {get; set;}

    public required string Organizer {get; set;}

    public required string PartyName {get; set;}

    public required List<BandResponse> Bands {get; set;}
}
