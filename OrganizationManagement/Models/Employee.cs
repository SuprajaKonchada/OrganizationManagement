using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace OrganizationManagement.Models;

/// <summary>
/// Represents an employee with basic and additional details.
/// </summary>
public class Employee
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string? FirstName { get; set; }

    [Required]
    public string? LastName { get; set; }

    public string? Department { get; set; }

    [JsonIgnore]
    [XmlIgnore]
    public DateTime CreatedAt { get; set; }

    public string? Position { get; set; }

    public string? PhoneNumber { get; set; }
}
