namespace ELMSApplication;
using System.ComponentModel.DataAnnotations;
using ELMSApplication.Properties.Attributes;

public class Leave
{
    [Key]
    public int LeaveId { get; set; }
     [Required(ErrorMessage ="Please enter Employee ID")]
    [RegularExpression(@"^[A-Z]{3}[0-9]{3}$", ErrorMessage = "Employee ID should start with 3 capital letters followed by 3 numeric values")]
    [StringLength(6, ErrorMessage = "Employee ID should be 6 characters long")]
    public string? EmployeeId { get; set; }
    // [Required(ErrorMessage = "Start date is required.")]
    // [DataType(DataType.DateTime)]
    // [RegularExpression( @"^(19\d{2}|20([0-1][0-9]|20))-(0[1-9]|1[0-2])-(0[1-9]|1\d|2[0-9]|3[0-1])$")]
    [Required(ErrorMessage ="Start date is required.")]
    [StartDate(ErrorMessage = "Invalid date format. Date must be in MM/dd/yyyy format.")]
    [DataType(DataType.DateTime)]

    public DateTime StartDate { get; set; }
     [Required(ErrorMessage = "End date is required.")]
    public DateTime EndDate { get; set; }
    [Required(ErrorMessage = "Leave type is required.")]
    [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Leave type should only contain letters and spaces.")]
    [StringLength(75)]
    public string? LeaveType { get; set; }
    [Required(ErrorMessage = "Status is required.")]
    [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Status should only contain letters and spaces.")]
    [StringLength(75)]
    public string? Status { get; set; }

}