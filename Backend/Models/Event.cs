public class Event : BaseEntity
{
    public required int EventID {get; set;}

    public required DateOnly StartDate {get; set;}

    public required TimeOnly StartTime {get; set;}

    public required string StartDay {get; set;}

    public required TimeOnly EntryTime {get; set;}

    public required double Price {get; set;}

    /* FK   PRTP_ID         INT          NOT NULL,
            VETP_ID         INT          NOT NULL,
            MITG_ID         INT          NOT NULL,
            GORT_ID         INT          NOT NULL */
}