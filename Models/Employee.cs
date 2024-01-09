namespace ELMSApplication;
using System.ComponentModel.DataAnnotations;

public class Employee
{
    [Key]
    public long Id { get; set; }
     [StringLength(75)]
    public string? EmployeeName { get; set; }
     [Required(ErrorMessage ="Please enter Employee ID")]
     [StringLength(75)]
    public string? EmployeeId { get; set; }
    [Required(ErrorMessage ="Please enter Password")]
    [DataType(DataType.Password)]
    [StringLength(75)]
    public string? Password { get; set; }
    public bool IsAdmin { get; set; }

}