public class Festival : BaseEntity
{
    public required int FestivalID {get; set;}

    public required DateOnly EndDate {get; set;}

    public required TimeOnly EndTime {get; set;}

    public required string EndDay {get; set;}
    
    public required string Organizer {get; set;}

    /* FK   VERA_ID (kp)       INT          NOT NULL,
            FSNM_ID (kp)       INT          NOT NULL */
}