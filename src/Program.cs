using System.ServiceProcess;

namespace PingService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Pingservice()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
