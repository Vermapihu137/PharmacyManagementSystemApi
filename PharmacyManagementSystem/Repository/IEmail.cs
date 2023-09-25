using System.Net;
using System.Net.Mail;

namespace PharmacyManagementSystem.Repository
{
    public class IEmail : IEmailInterface
    {
        private IConfiguration configuration;
        public IEmail(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void SendEmail(string toEmail, string subject, string body)
        {
            var emailSettings = configuration.GetSection("EmailSettings");
            using (var client = new SmtpClient())
            {
                client.Host = emailSettings["SmtpServer"];
                client.Port = int.Parse(emailSettings["Port"]);
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(emailSettings["Username"], emailSettings["Password"]);
                client.EnableSsl = true;

                using (var emailMessage = new MailMessage())
                {
                    emailMessage.To.Add(new MailAddress(toEmail));
                    emailMessage.From = new MailAddress(emailSettings["FromAddress"]);
                    emailMessage.Subject = subject;
                    emailMessage.Body = body;
                    emailMessage.IsBodyHtml = true;
                    client.Send(emailMessage);
                }
            }
        }
    }
}
