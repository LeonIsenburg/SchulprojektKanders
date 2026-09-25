namespace Backend.Models.Requests;

public class PartyRequest
{
    public required DateOnly EndDate {get; set;}

    public required TimeOnly EndTime {get; set;}

    public required string Organizer {get; set;}

    public required string PartyName {get; set;}

    public required List<BandRequest> Bands {get; set;}
}
