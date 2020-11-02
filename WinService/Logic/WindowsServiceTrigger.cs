using System;

namespace WinService.Logic
{
    public class WindowsServiceTrigger : IWindowsServiceTrigger
    {
        readonly string serviceName = string.Empty;
        readonly string logSourceName = string.Empty;
        readonly string logName = string.Empty;
        readonly Action _action;
        public WindowsServiceTrigger(string serviceName, string logSourceName, string logName, Action action)
        {
            this.serviceName = serviceName;
            this.logSourceName = logSourceName;
            this.logName = logName;
            _action = action;
        }

        public void Start()
        {
            Program.Main(serviceName, logSourceName, logName, _action);
        }
    }
}
