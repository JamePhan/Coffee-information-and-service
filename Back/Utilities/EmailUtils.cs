using MailKit.Net.Smtp;
using MimeKit;

namespace Back.Utilities
{
    public class EmailUtils
    {
        private readonly IConfiguration _configuration;

        public EmailUtils(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEmail(EmailEncapsulation sendTo, EmailDetails details)
        {
            try
            {
                string from = _configuration.GetSection("Settings").GetSection("Email").Value;
                string password = _configuration.GetSection("Settings").GetSection("SMTPPassword").Value;

                MimeMessage email = new();
                email.From.Add(MailboxAddress.Parse(from));
                email.To.Add(MailboxAddress.Parse(sendTo.Email));
                email.Subject = details.Subject;
                email.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
                {
                    Text = details.Body,
                };

                using SmtpClient smtp = new();
                smtp.Connect("smtp.gmail.com", 587, false);
                smtp.Authenticate(from, password);
                smtp.Send(email);
                smtp.Disconnect(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    public class EmailEncapsulation
    {
        public string Email { get; set; }
    }

    public class EmailDetails
    {
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}