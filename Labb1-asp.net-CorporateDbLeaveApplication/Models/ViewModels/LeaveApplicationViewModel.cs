using static Labb1_asp.net_CorporateDbLeaveApplication.Models.LeaveApplication;
using System.ComponentModel.DataAnnotations;

namespace Labb1_asp.net_CorporateDbLeaveApplication.Models.ViewModels
{
    public class LeaveApplicationViewModel
    {
        public int EmployeeId { get; set; }
        public Employee? employee { get; set; }
        public int LeaveApplicationId { get; set; }

        [Required]
        [Display(Name = "Type of leave")]
        public LeaveType TypeOfLeave { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Start date")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "End date")]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "Date of application submission")]
        public DateTime ApplicationSubmissionDate { get; set; }

        [Required]
        [Display(Name = "Application status")]
        public ApplicationStatus ApplicationStatus { get; set; }
    }
}
