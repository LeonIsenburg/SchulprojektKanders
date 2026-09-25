namespace Backend.Models.Requests;

public class ConcertRequest
{
    public required string Organizer {get; set;}

    public required List<BandRequest> Bands {get; set;}
}
