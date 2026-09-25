namespace Backend.Models.Responses;

public class ConcertResponse
{
    public required string Organizer {get; set;}

    public required List<BandResponse> Bands {get; set;}
}
