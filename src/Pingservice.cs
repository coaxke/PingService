using System;
using System.Diagnostics;
using System.ServiceProcess;
using System.Timers;
using System.Net.NetworkInformation;
using PingService.Configuration;

namespace PingService
{
    public partial class Pingservice : ServiceBase
    {
        IConfiguration _config = new PingConfiguration();

        public Pingservice()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            PingServiceEventLog().WriteEntry("Starting Ping service");

            Timer Pingtimer = new Timer();
            Pingtimer.Interval = _config.PingInterval * 1000;
            Pingtimer.Elapsed += new ElapsedEventHandler(ExecutePing);
            Pingtimer.Start();
        }

        protected override void OnStop()
        {
            PingServiceEventLog().WriteEntry("Stopping Ping service");
        }

        public void ExecutePing(object sender, EventArgs e)
        {
            Ping PingSender = new Ping();

            PingReply PingReply = PingSender.Send(_config.PingDestination);

            if (PingReply.Status == IPStatus.Success)
            {
                if (_config.VerboseLoggingToEventLog)
                {
                    PingServiceEventLog().WriteEntry(string.Format("Sucessfully sent a ping to {0}", _config.PingDestination));
                }
                //Handle success if needed:
            }
            else
            {
                if (_config.VerboseLoggingToEventLog)
                {
                    PingServiceEventLog().WriteEntry(string.Format("Failed to send a ping to {0}", _config.PingDestination));
                }
                //handle failure if needed:
            }
        }

        private EventLog PingServiceEventLog()
        {
            EventLog PingServiceEventLog = new EventLog
            {
                Source = "Pingservice",
                Log = "Application"
            };

            return PingServiceEventLog;
        }
    }
}
