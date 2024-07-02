using Labb1_asp.net_CorporateDbLeaveApplication.Data;
using Labb1_asp.net_CorporateDbLeaveApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labb1_asp.net_CorporateDbLeaveApplication.Utility
{
    public static class DbInitializer
    {
        private static readonly Random _random = new();

        public static async Task Initialize(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();

            if (context.Employees.Any())
            {
                return;   // Database has already been seeded
            }

            // Define roles
            string[] roleNames = { "Employee", "Manager" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Create and add users to roles
            var users = new IdentityUser[]
                {
                    new IdentityUser { UserName = "emma.andersson@corporatedb.com", Email = "emma.andersson@corporatedb.com", EmailConfirmed = true },
                    new IdentityUser { UserName = "johan.lindberg@corporatedb.com", Email = "johan.lindberg@corporatedb.com", EmailConfirmed = true },
                    new IdentityUser { UserName = "anna.karlsson@corporatedb.com", Email = "anna.karlsson@corporatedb.com", EmailConfirmed = true },
                    new IdentityUser { UserName = "erik.nilsson@corporatedb.com", Email = "erik.nilsson@corporatedb.com", EmailConfirmed = true },
                    new IdentityUser { UserName = "maria.gustafsson@corporatedb.com", Email = "maria.gustafsson@corporatedb.com", EmailConfirmed = true },
                    new IdentityUser { UserName = "amanda.eriksson@corporatedb.com", Email = "amanda.eriksson@corporatedb.com", EmailConfirmed = true },
                    new IdentityUser { UserName = "sara.persson@corporatedb.com", Email = "sara.persson@corporatedb.com", EmailConfirmed = true },
                    new IdentityUser { UserName = "mattias.berg@corporatedb.com", Email = "mattias.berg@corporatedb.com", EmailConfirmed = true },
                    new IdentityUser { UserName = "linnea.larsson@corporatedb.com", Email = "linnea.larsson@corporatedb.com", EmailConfirmed = true },
                    new IdentityUser { UserName = "daniel.svensson@corporatedb.com", Email = "daniel.svensson@corporatedb.com", EmailConfirmed = true },
                    new IdentityUser { UserName = "hanna.olofsson@corporatedb.com", Email = "hanna.olofsson@corporatedb.com", EmailConfirmed = true },
                    new IdentityUser { UserName = "peter.johansson@corporatedb.com", Email = "peter.johansson@corporatedb.com", EmailConfirmed = true },
                    new IdentityUser { UserName = "jonas.pettersson@corporatedb.com", Email = "jonas.pettersson@corporatedb.com", EmailConfirmed = true },
                    new IdentityUser { UserName = "magnus.bjork@corporatedb.com", Email = "magnus.bjork@corporatedb.com", EmailConfirmed = true },
                    new IdentityUser { UserName = "ida.eklund@corporatedb.com", Email = "ida.eklund@corporatedb.com", EmailConfirmed = true }
                };


            foreach (var user in users)
            {
                var result = await userManager.CreateAsync(user, "Password123!");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Employee");
                }
            }

            var managers = new IdentityUser[]
            {
                new IdentityUser { UserName = "malin.lindberg@corporatedb.com", Email = "malin.lindberg@corporatedb.com", EmailConfirmed = true }
                // Add more managers as needed
            };

            foreach (var manager in managers)
            {
                var result = await userManager.CreateAsync(manager, "Password123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(manager, "Manager");
                }
            }

            // Save changes to get user IDs
            await context.SaveChangesAsync();

            // Create employees linked to the users
            var employees = new List<Employee>();
            foreach (var user in users)
            {
                employees.Add(new Employee
                {
                    ApplicationUserId = user.Id,
                    FirstName = user.UserName.Split('@')[0].Split('.')[0], // Extract first name from email
                    LastName = user.UserName.Split('@')[0].Split('.').Length > 1 ? user.UserName.Split('@')[0].Split('.')[1] : "Lastname",
                    BirthDate = new DateTime(_random.Next(1960, 2000), _random.Next(1, 12), _random.Next(1, 28)), // Example birth date
                    Email = user.Email
                });
            }

            foreach (var manager in managers)
            {
                employees.Add(new Employee
                {
                    ApplicationUserId = manager.Id,
                    FirstName = manager.UserName.Split('@')[0].Split('.')[0], // Extract first name from email
                    LastName = manager.UserName.Split('@')[0].Split('.').Length > 1 ? manager.UserName.Split('@')[0].Split('.')[1] : "Lastname",
                    BirthDate = new DateTime(_random.Next(1960, 2000), _random.Next(1, 12), _random.Next(1, 28)), // Example birth date
                    Email = manager.Email
                });
            }

            await context.Employees.AddRangeAsync(employees);
            await context.SaveChangesAsync();

            // Create leave applications for some of the employees
            var leaveApplications = new List<LeaveApplication>();
            for (int i = 0; i < 25; i++) // Create 25 leave applications
            {
                var applicationDates = GenerateRandomDates();
                var randomEmployee = employees[_random.Next(employees.Count)]; // Select a random employee
                var randomStatus = (ApplicationStatus)_random.Next(Enum.GetValues(typeof(ApplicationStatus)).Length); // Randomly choose an application status

                leaveApplications.Add(new LeaveApplication
                {
                    StartDate = applicationDates.startDate,
                    EndDate = applicationDates.endDate,
                    ApplicationSubmissionDate = applicationDates.appliedDate,
                    TypeOfLeave = (LeaveType)_random.Next(Enum.GetValues(typeof(LeaveType)).Length),
                    ApplicationStatus = randomStatus,
                    FkEmployeeId = randomEmployee.EmployeeId
                });
            }

            await context.LeaveApplications.AddRangeAsync(leaveApplications);
            await context.SaveChangesAsync();
        }

        private static (DateTime appliedDate, DateTime startDate, DateTime endDate) GenerateRandomDates()
        {
            DateTime appliedDate = DateTime.Now.AddDays(-_random.Next(1, 120));
            DateTime startDate = appliedDate.AddDays(_random.Next(1, 30));
            DateTime endDate = startDate.AddDays(_random.Next(1, 90));

            return (appliedDate, startDate, endDate);
        }
    }
}
