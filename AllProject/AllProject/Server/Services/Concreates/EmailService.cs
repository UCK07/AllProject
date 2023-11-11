using AllProject.Server.Services.Interfaces;
using System.Net.Mail;
using System.Net;

namespace AllProject.Server.Services.Concreates
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration) { _configuration = configuration; }
        public Task<bool> SendEmail(string to, string subJect, string message)
        {
            var appsetting = _configuration.GetSection("EmailSettings");
            var port = appsetting["Port"];
            var emailAddress = appsetting["EmailAddress"];
            var pass = appsetting["Password"];
            var host = appsetting["Host"];
            var title = appsetting["Title"];
            var smtpSSL = appsetting["Ssl"];
            var msg = new MailMessage { IsBodyHtml = true };
            msg.To.Add(to);
            msg.From = new MailAddress(emailAddress, title);
            msg.Body = message;
            try
            {
                var smp = new SmtpClient
                {
                    Credentials = new NetworkCredential(emailAddress, pass),
                    Port = Convert.ToInt32(port),
                    Host = host,
                    EnableSsl = Convert.ToBoolean(smtpSSL),
                    UseDefaultCredentials = false
                };
                smp.Send(msg);
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                return Task.FromResult(false);
            }
        }
    }
}