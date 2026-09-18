using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class BaseEntity
{
    [Key]
    public required Guid Guid {get; set;}
}