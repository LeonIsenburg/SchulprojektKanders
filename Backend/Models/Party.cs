public class Party : BaseEntity
{
    public required int PartyID {get; set;}

    public required DateOnly EndDay {get; set;}

    public required TimeOnly EndTime {get; set;}

    public required string EndDay {get; set;}

    public required string Organizer {get; set;}
}