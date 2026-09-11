public class FestivalBand : BaseEntity
{
    public required int FestivalBandID {get; set;}

    public required DateOnly Date {get; set;}

    public required TimeOnly Time {get; set;}

    public required string Day {get; set;}

    /* FK    FSTV_ID (Festival)   INT         NOT NULL,
             BAND_ID (Band)   INT         NOT NULL, */
}