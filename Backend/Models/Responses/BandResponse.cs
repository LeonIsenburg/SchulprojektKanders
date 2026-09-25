namespace Backend.Models.Responses;

public class BandResponse
{
    public required string Name {get; set;}

    public required List<MusicianResponse> Musicians {get; set;}

    public required List<string> Genres {get; set;}

    public required List<string> Songs {get; set;}
}
