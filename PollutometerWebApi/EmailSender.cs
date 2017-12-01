using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace PollutometerWebApi
{
    public class EmailSender
    {
        public EmailSender() {}

        public static void SendEmail(string gasName, double max)
        {
            try
            {
                MailMessage mail = new MailMessage("***REMOVED***", "***REMOVED***@edu.easj.dk");
                SmtpClient client = new SmtpClient()
                {
                    Host = "mail.cock.li",
                    Port = 587,
                    EnableSsl = true,
                    Timeout = 100,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential("***REMOVED***", "***REMOVED***")
                };
				mail.Subject = $"Pollutometer warning - {DateTime.Now}";
				mail.IsBodyHtml = true;
                mail.Body = "<h3>WARNING!</h3>\n" +
                    "\n" +
                    "<img src=\"https://i.imgflip.com/20b4q2.jpg\"/>\n" +
                    "\n" +
                    $"<p>The warning was triggered by {gasName}.</p>\n" +
                    $"<p>Air quality index: {max}</p>";
                client.Send(mail);
            }
            catch(Exception ex)
            {
                Console.Write(ex.Message);
            }
        }
    }
}