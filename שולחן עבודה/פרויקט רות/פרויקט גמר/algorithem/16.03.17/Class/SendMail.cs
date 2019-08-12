using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace _16._03._17
{
    class SendMail
    {
        public static void Send(string to, string html,string name)
        {
            //File.Create("tmp.html");
            File.WriteAllText("tmp.html", html);
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
            mail.From = new MailAddress("projectmbyht@gmail.com");
            mail.To.Add(to);
            mail.Subject = "on time "+name;
            //LinkedResource inline = new LinkedResource("logo.png");
            //inline.ContentId = Guid.NewGuid().ToString();
            //mail.Body = String.Format(
            //     @"<img src=""cid:{0}"" />", inline.ContentId);

            mail.AlternateViews.Add(getEmbeddedImage("logo.png"));

            Attachment item = new Attachment("tmp.html");
            mail.Attachments.Add(item);
            //SmtpServer.Port = 587;
            SmtpServer.UseDefaultCredentials = false;

            SmtpServer.Credentials = new System.Net.NetworkCredential("projectmbyht@gmail.com", "project123");
            SmtpServer.EnableSsl = true;
            //SmtpServer.Timeout = 1000;
            SmtpServer.Send(mail);
            item.Dispose();

            File.Delete("tmp.html");
                    }
        private static AlternateView getEmbeddedImage(String filePath)
        {
            LinkedResource inline = new LinkedResource(filePath);
            inline.ContentId = Guid.NewGuid().ToString();
            string htmlBody = @"<img src='cid:" + inline.ContentId + @"'/>";
            AlternateView alternateView = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);
            alternateView.LinkedResources.Add(inline);
            return alternateView;
        }
    }
}
