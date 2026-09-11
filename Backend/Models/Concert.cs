public class Concert : BaseEntity
{
    public required int ConcertID {get; set;}

    public required string Organizer {get; set;}

    /* FK   VERA_ID (Organizer)    INT          NOT NULL,
            BAND_ID (Band)      INT          NOT NULL */
}