namespace Backend.Models.Requests;

public class TicketsRequest
{
    public double? Price {get; set;}

    public string? Currency {get; set;}

    public bool? Presale {get; set;}

    public string? Url {get; set;}
}
