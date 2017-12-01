using System;
using System.Net.Mail;

namespace PollutometerWebApi
{
    public class EmailSender
    {
        public EmailSender() {}

        public static void SendEmail()
        {
            MailMessage mail = new MailMessage("***REMOVED***", "***REMOVED***@edu.easj.dk");
            SmtpClient client = new SmtpClient()
            {
                Host = "mail.cock.li",
                Port = 465,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential("***REMOVED***", "***REMOVED***")
            };
            mail.Subject = "this is a test email.";
            mail.Body = "this is my test email body";
            client.Send(mail);
        }
    }
}