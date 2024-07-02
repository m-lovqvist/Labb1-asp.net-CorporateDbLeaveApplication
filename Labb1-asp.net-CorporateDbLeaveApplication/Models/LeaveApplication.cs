using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Labb1_asp.net_CorporateDbLeaveApplication.Models
{
    public class LeaveApplication
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LeaveApplicationId { get; set; }

        [Required]
        [Display(Name = "Type of leave")]
        public required LeaveType TypeOfLeave { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date of application submission")]
        public DateTime ApplicationSubmissionDate { get; set; } = DateTime.Now;

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Start date")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "End date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Employee")]
        [ForeignKey("Employee")]
        public int FkEmployeeId { get; set; }

        public Employee? Employee { get; set; }

        public ApplicationStatus ApplicationStatus { get; set; }

    }
        public enum LeaveType
        {
            [Display(Name = "Annual leave")]
            Annual,
            [Display(Name = "Vacation day")]
            Vacation,
            [Display(Name = "Parental leave")]
            Parental,
            [Display(Name = "Sick leave")]
            Sick,
            [Display(Name = "Care of child")]
            CareOfChild
        }

        public enum ApplicationStatus
        {
            Pending,
            Approved,
            Denied
        }
}
