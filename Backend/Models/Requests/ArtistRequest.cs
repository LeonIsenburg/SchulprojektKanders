namespace Backend.Models.Requests;

public class ArtistRequest
{
    public string? Name {get; set;}

    public List<string?>? Genres {get; set;}

    public List<MemberRequest?>? Members {get; set;}

    public List<string?>? Songs {get; set;}

    public DateTime? Performance {get; set;}
}
