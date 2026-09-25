namespace Backend.Models.Responses;

public class BandPerformanceResponse
{
    public required BandResponse Band {get; set;}

    public DateOnly? Date {get; set;}

    public TimeOnly? Time {get; set;}

    public string? Day {get; set;}
}
