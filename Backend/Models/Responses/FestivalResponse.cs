namespace Backend.Models.Responses;

public class FestivalResponse
{
    public required DateOnly EndDate {get; set;}

    public required TimeOnly EndTime {get; set;}

    public required string EndDay {get; set;}

    public required string Organizer {get; set;}

    public required string FestivalName {get; set;}

    public required List<BandPerformanceResponse> Bands {get; set;}
}
