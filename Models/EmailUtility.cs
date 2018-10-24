using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace BrightWorks.Models
{
    public class EmailUtility
    {
        public static void sendMail(String Toemail, String body)
        {
            // Command line argument must the the SMTP host.
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.UseDefaultCredentials = true;
            client.Credentials = new System.Net.NetworkCredential("serge.felixx@gmail.com", "Squarehard743");

            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            MailAddress from = new MailAddress("serge.felixx@gmail.com");
            MailAddress to = new MailAddress(Toemail);
            MailMessage message = new MailMessage(from, to);
            message.Subject = "BrightWorks Notificaions.";
            message.Body = body;

            message.BodyEncoding = System.Text.Encoding.UTF8;
            client.Send(message);

            message.Dispose();
            Console.WriteLine("Goodbye.");
        }
    }
}