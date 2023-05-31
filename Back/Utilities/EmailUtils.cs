using MimeKit;

namespace Back.Utilities
{
    public class EmailUtils
    {
        public void SendEmail(EmailEncapsulation sendTo, TextPart body)
        {
        }
    }

    public class EmailEncapsulation
    {
        public string Email { get; set; }
    }
}