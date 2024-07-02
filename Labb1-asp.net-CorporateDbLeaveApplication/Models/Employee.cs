using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Labb1_asp.net_CorporateDbLeaveApplication.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; }
        public string ApplicationUserId { get; set; }
        [Required(ErrorMessage = "First name is required")]
        [StringLength(15, ErrorMessage = "15 character maximum")]
        [Display(Name = "First Name")]
        public required string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        [StringLength(25, ErrorMessage = "25 character maximum")]
        [Display(Name = "Last Name")]
        public required string LastName { get; set;}
        [Required(ErrorMessage = "Date of birth is required")]
        [Display(Name = "Date of birth")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [StringLength(50, ErrorMessage = "50 character maximum")]
        [Display(Name = "Email")]
        public required string Email { get; set; }

        //Navigation property for related entities LeaveApplications
        public ICollection<LeaveApplication>? LeaveApplications { get; set; }
    }
}
