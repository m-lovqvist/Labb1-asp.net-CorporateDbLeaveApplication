using Microsoft.AspNetCore.Identity.UI.Services;

namespace Labb1_asp.net_CorporateDbLeaveApplication.Utilities
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //Add email logic
            return Task.CompletedTask;
        }
    }
}
