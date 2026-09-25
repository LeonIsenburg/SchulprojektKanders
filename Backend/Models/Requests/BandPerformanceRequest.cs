namespace Backend.Models.Requests;

public class BandPerformanceRequest
{
    public required BandRequest Band {get; set;}

    public DateOnly? Date {get; set;}

    public TimeOnly? Time {get; set;}

    public string? Day {get; set;}
}
