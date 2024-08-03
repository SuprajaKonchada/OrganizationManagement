using System.ComponentModel.DataAnnotations;

namespace OrganizationManagement.Models;

/// <summary>
/// Represents the detailed information of an employee.
/// </summary>
public class EmployeeDetails
{
    [Key]
    public int Id { get; set; }

    public string? FullName { get; set; }
    public string? Department { get; set; }
    public string? Position { get; set; }
    public string? PhoneNumber { get; set; }
}
