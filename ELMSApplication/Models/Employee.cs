namespace ELMSApplication;
using System.ComponentModel.DataAnnotations;
using ELMSApplication.Properties.Attributes;

public class Employee
{
    [Key]
    public long Id { get; set; }
     [StringLength(75)]
    public string? EmployeeName { get; set; }
    //  [Required(ErrorMessage ="Please enter Employee ID")]
    //  [StringLength(75)]
    // [RegularExpression(@"^[A-Z0-9]+$", ErrorMessage = "Code should only contain capital letters and numeric values")]
    // public string? EmployeeId { get; set; }
    [Required(ErrorMessage ="Please enter Employee ID")]
    [RegularExpression(@"^[A-Z]{3}[0-9]{3}$", ErrorMessage = "Employee ID should start with 3 capital letters followed by 3 numeric values")]
    [StringLength(6, ErrorMessage = "Employee ID should be 6 characters long")]
    public string? EmployeeId { get; set; }
    

    [Required(ErrorMessage ="Please enter Password")]
    [PasswordValidation(ErrorMessage = "Your password must be at least 6 characters long, contain 1 uppercase letter, 1 lowercase letter, 1 number, and 1 special character.")]
    [DataType(DataType.Password)]
    [StringLength(75)]
    public string? Password { get; set; }
    public bool IsAdmin { get; set; }
    public bool KeepLoggedIn { get;  internal set; }

}