using System.Net.Mail;

namespace OnePage.SendEmail.Models
{
    public class MailStructure
    {
        public string Sender { get; set; }
        public string Recipient { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<Attachment> Attachments { get; set; }
    }
}
