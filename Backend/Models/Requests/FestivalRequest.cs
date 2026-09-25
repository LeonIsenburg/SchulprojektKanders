namespace Backend.Models.Requests;

public class FestivalRequest
{
    public required DateOnly EndDate {get; set;}

    public required TimeOnly EndTime {get; set;}

    public required string Organizer {get; set;}

    public required string FestivalName {get; set;}

    public required List<BandPerformanceRequest> Bands {get; set;}
}
