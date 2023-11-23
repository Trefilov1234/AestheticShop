using MailKit.Net.Smtp;
using MimeKit;

namespace AestheticShop.Areas.Admin.Services
{
    public class MailSenderService
    {
        public async Task Send(string email, string subject, string content)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("AestheticShop1", "postmaster@sandbox804c965fe8434b758f45ded045c4aa3d.mailgun.org"));
            emailMessage.To.Add(new MailboxAddress("Client", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = content };
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.mailgun.org", 587, false);
                await client.AuthenticateAsync("postmaster@sandbox804c965fe8434b758f45ded045c4aa3d.mailgun.org",
                    "39e5145ee5340312274aab8bab7d4820-1c7e8847-3fdb9805");
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}
