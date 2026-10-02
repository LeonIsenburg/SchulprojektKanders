namespace Backend.Models.Responses;

public class ArtistResponse
{
    public required string Name {get; set;}

    public required List<string> Genres {get; set;}

    public required List<MemberResponse> Members {get; set;}

    public required List<string> Songs {get; set;}

    public DateTime? Performance {get; set;}
}
