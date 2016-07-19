using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace ProjectManagement.Services
{
    public class GmailService
    {
        public void Send(string toEmail, string toName, string subjectText, string bodyText)
        {
            var fromAddress = new MailAddress("bradit.d1afdigg@gmail.com", "PM Team");
            var toAddress = new MailAddress(toEmail, toName);
            const string fromPassword = "Sedc123^";
            string subject = subjectText;
            string body = bodyText;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                Timeout = 20000
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
    }
}