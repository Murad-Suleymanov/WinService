using System.ServiceProcess;

namespace WinService
{
    public class GenericWinService : ServiceBase
    {
        public GenericWinService(string serviceName)
        {
            ServiceName = serviceName;
        }

        protected override void OnStart(string[] args)
        {
            Program.Start();
        }

        protected override void OnStop()
        {
            Program.Stop();
        }
    }
}
