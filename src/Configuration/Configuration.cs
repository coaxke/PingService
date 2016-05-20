using System.Configuration;
using System.Net;
using System.ServiceProcess;
namespace PingService.Configuration
{
    public interface IConfiguration
    {
        bool VerboseLoggingToEventLog { get; }
        IPAddress PingDestination { get; }
        int PingInterval { get; }
    }


    public class PingConfiguration : IConfiguration
    {
        public bool VerboseLoggingToEventLog
        {
            get
            {
                bool VerboseLogging;

                if(!bool.TryParse(ConfigurationManager.AppSettings["VerboseLoggingToEventLog"], out VerboseLogging))
                {
                    VerboseLogging = false;
                }

                return VerboseLogging;
            }
        }

        public IPAddress PingDestination
        {
            get
            {
                IPAddress PingAddress;

                if (!IPAddress.TryParse(ConfigurationManager.AppSettings["PingDestination"], out PingAddress))
                {
                    ServiceController svc = new ServiceController("PingService");
                    svc.Stop();
                }

                return PingAddress;
            }

        }

        public int PingInterval
        {
            get
            {
                int PingInterval;

                if (!int.TryParse(ConfigurationManager.AppSettings["PingIntervalSecs"], out PingInterval))
                {
                    PingInterval = 10;
                }

                return PingInterval;
            }
        }
    }
}
