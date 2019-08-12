using BL;
using Entities.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;

namespace MonitorService
{
    public partial class MonitorService : ServiceBase
    {
        EventLog eventLog;
        public static int eventId = 1;
        private System.Threading.Timer timer = null;
        public MonitorService()
        {
            InitializeComponent();
            eventLog = new System.Diagnostics.EventLog();
            // Create a file to write the log
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
            // set the time the service start and setting the time period for each round
            TimeSpan scheduledRunTime = new TimeSpan(20, 8, 0), timeBetweenEachRun = new TimeSpan(0, 5, 0);
            double current = DateTime.Now.TimeOfDay.TotalMilliseconds;
            double scheduledTime = scheduledRunTime.TotalMilliseconds;
            double intervalPeriod = timeBetweenEachRun.TotalMilliseconds;
            // calculates the first execution of the method, either its today at the scheduled time or tomorrow (if scheduled time has already occurred today)
            double firstExecution = current > scheduledTime ? intervalPeriod - (current - scheduledTime) : scheduledTime - current;
            // create callback - this is the method that is called on every interval
            TimerCallback callback = new TimerCallback(Monitoring);
            // create timer
            timer = new System.Threading.Timer(callback, null, Convert.ToInt32(firstExecution), Convert.ToInt32(intervalPeriod));
            // initialization the mapper
            RequestBL.OnInitMapper(eventLog);
        }

        protected void Monitoring(object sender)
        {
            eventLog.WriteEntry("MonitorService:In GetAllRelevantRequests.");
            // get all relevant requests
            List<RequestDto> requestDtos = RequestBL.GetAllRelevantRequests(eventLog);
            // for each request
            foreach (var request in requestDtos)
            {
                try
                {
                    request.eventLog = eventLog;
                    // create thread and initialization the function
                    Thread thread = new Thread(RequestBL.ThreadFunction);
                    // run the function
                    thread.Start(request);
                }
                catch (Exception ex)
                {
                    eventLog.WriteEntry(ex.ToString());
                }
            }
            eventLog.WriteEntry("Monitoring the System", EventLogEntryType.Information, eventId++);
        }

        protected override void OnStop()
        {
            eventLog.WriteEntry("In OnStop.");
        }

    }
}


