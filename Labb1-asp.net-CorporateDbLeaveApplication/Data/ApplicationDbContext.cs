using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Labb1_asp.net_CorporateDbLeaveApplication.Models;

namespace Labb1_asp.net_CorporateDbLeaveApplication.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Labb1_asp.net_CorporateDbLeaveApplication.Models.Employee> Employees { get; set; } = default!;
        public DbSet<Labb1_asp.net_CorporateDbLeaveApplication.Models.LeaveApplication> LeaveApplications { get; set; } = default!;
    }
}
