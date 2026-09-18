public class ValidLocation : BaseEntity
{
    public required int ValidLocationID {get; set;}

    public required string PostalCode {get; set;}

    public required string Street {get; set;}

    public required string HouseNumber {get; set;}
}