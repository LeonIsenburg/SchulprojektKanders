namespace Backend.Models.Requests;

public class BandRequest
{
    public required string Name {get; set;}

    public required List<MusicianRequest> Musicians {get; set;}

    public List<string>? Genres {get; set;}

    public List<string>? Songs {get; set;}
}
