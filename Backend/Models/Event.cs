public class Event : BaseEntity
{
    public required int EventID {get; set;}

    public required DateOnly StartDate {get; set;}

    public required TimeOnly StartTime {get; set;}

    public required string StartDay {get; set;}

    public required TimeOnly EntryTime {get; set;}

    public required double Price {get; set;}

    /* FK   PRTP_ID (Party?)         INT          NOT NULL,
            VETP_ID (kp mehr, muss gucken)        INT          NOT NULL,
            MITG_ID (member)         INT          NOT NULL,
            GORT_ID (valid location)         INT          NOT NULL */
}