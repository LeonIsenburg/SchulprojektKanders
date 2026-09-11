public class ValidLocation : BaseEntity
{
    public required int ValidLocationID {get; set;}

    public required string PostalCode {get; set;}

    public required string Street {get; set;}

    public required string HouseNumber {get; set;}

    /* FK   STDT_ID (kp)   INT          NOT NULL,
            STTL_ID (kp)   INT          NOT NULL,
            REGI_ID (kp)   INT          NOT NULL,
            BULA_ID (kp)   INT          NOT NULL,
            VAOT_ID (kp)  INT          NOT NULL */
}