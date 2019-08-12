using BL;
using Entities.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Timers;
using System.IO;

namespace MonitorService
{

    public partial class MonitorService : ServiceBase
    {
        EventLog eventLog;
        public static int eventId = 1;

        Timer timer = new Timer();
        public MonitorService()
        {
            InitializeComponent();

            eventLog = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists("MonitorServiceSource"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "MonitorServiceSource", "MonitorServiceLog");
            }
            eventLog.Source = "MonitorServiceSource";
            eventLog.Log = "MonitorServiceLog";

        }

        protected override void OnStart(string[] args)
        {
            eventLog.WriteEntry("In OnStart.");
            RequestBL.OnInitMapper();
            timer.Interval = 300000; // 5 minute
            timer.Elapsed += new ElapsedEventHandler(GetAllRelevantRequests);
            timer.Start();
            //timer.Enabled = true;
        }
        protected void GetAllRelevantRequests(object sender, System.Timers.ElapsedEventArgs args)
        {
            eventLog.WriteEntry("MonitorService:In GetAllRelevantRequests.");
            List<RequestDto> requestDtos = RequestBL.GetAllRelevantRequests(eventLog);
            foreach (var request in requestDtos)
            {
                byte[] newPlay = Play(request.recording_stream);
                bool isChange = Compere(newPlay, request.file_stream);
                if (isChange)
                {
                    SendMail(request.userMail);
                }
            }
            eventLog.WriteEntry("Monitoring the System", EventLogEntryType.Information, eventId++);
            using (StreamWriter outputFile = new StreamWriter(Path.Combine("M:\\", eventId + "test.txt"), true))
            {
                outputFile.WriteLine(eventId);
            }

        }

        private bool Compere(byte[] newPlay, byte[] file_stream)
        {
            eventLog.WriteEntry("In Compere.");
            //TODO הפונקציה מקבלת 2 חלקי דף וצריכה להשוות אותם ובמידה ויש שינוי תחזיר אמת
            return true;
        }

        protected byte[] Play(byte[] recording)
        {
            eventLog.WriteEntry("In Play.");
            //TODOהפונקציה מקבלת הקלטה שהתוסף הוציא מומרת למערך של בתים 
            //צריך גלישה רובוטית 
            // הפונקציה מחזירה את הגלישה החדשה לצורך השוואה
            return null;
        }

        protected override void OnStop()
        {
            eventLog.WriteEntry("In OnStop.");
        }
        protected void SendMail(string userMail)
        {
            eventLog.WriteEntry("In SendMail.");
            try
            {
                SmtpClient client = new SmtpClient();
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("ontimemonitor@gmail.com", "onTime123");
                MailMessage mm = new MailMessage("ontimemonitor@gmail.com", userMail, "test", "<h1>new message</h1>");
                mm.IsBodyHtml = true;
                mm.BodyEncoding = UTF8Encoding.UTF8;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;


                client.Send(mm);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }

    }
}


