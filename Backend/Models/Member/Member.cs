using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models.Member;

[Index(nameof(Username), IsUnique = true)]
[Index(nameof(Nickname), IsUnique = true)]
public class Member : BaseEntity
{
    public required int MemberID {get; set;}

    public required string Username {get; set;}

    public required string Password {get; set;}

    public required string Nickname {get; set;}

    public required string FirstName {get; set;}

    public required string LastName {get; set;}

    public required string EMail {get; set;}

    public required DateOnly Birthday {get; set;}

    public required string Gender {get; set;}

    public required string PhoneNumber {get; set;}

    public required string Street {get; set;}

    public required string HouseNumber {get; set;}

    public required string PostalCode {get; set;}

    public required string Location {get; set;}

    public required Role Role {get; set;}

    public ICollection<Backend.Models.Event.Event> OrganizedEvents {get; set;} = new List<Backend.Models.Event.Event>();
}