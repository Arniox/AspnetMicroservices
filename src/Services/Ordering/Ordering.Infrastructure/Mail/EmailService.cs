using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Mail
{
    public class EmailService : IEmailService
    {
        //Objects
        private EmailSettings _emailSettings { get; }
        private ILogger<EmailService> _logger { get; }
        
        //Constructor with IOptions
        public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
        {
            _emailSettings = emailSettings.Value;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        //Send Mail with SendGrit
        public async Task<bool> SendEmail(Email email)
        {
            //Create client
            var client = new SendGridClient(_emailSettings.ApiKey);

            //Email
            var subject = email.Subject;
            var to = new EmailAddress(email.To);
            var emailBody = email.Body;

            //From URI
            var from = new EmailAddress
            {
                Email = _emailSettings.FromAddress,
                Name = _emailSettings.FromName
            };

            //Send message
            var sendGridMessage = MailHelper.CreateSingleEmail(from, to, subject, emailBody, emailBody);
            var response = await client.SendEmailAsync(sendGridMessage);
            _logger.LogInformation("Email Sent.");

            //If no issue
            if (response.StatusCode == HttpStatusCode.Accepted || response.StatusCode == HttpStatusCode.OK)
                return true;

            //If issue
            _logger.LogError("Email Failed to be Sent.");
            return false;
        }
    }
}
