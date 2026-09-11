public class ValidLocation : BaseEntity
{
    public required int ValidLocationID {get; set;}

    public required string PostalCode {get; set;}

    public required string Street {get; set;}

    public required string HouseNumber {get; set;}

    /* FK   STDT_ID    INT          NOT NULL,
            STTL_ID    INT          NOT NULL,
            REGI_ID    INT          NOT NULL,
            BULA_ID    INT          NOT NULL,
            VAOT_ID    INT          NOT NULL */
}