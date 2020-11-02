using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace WinService
{
    [RunInstaller(true)]
    public partial class GenericWinServiceInstaller : Installer
    {
        public GenericWinServiceInstaller()
        {
            InitializeComponent();

            var processInstaller = new ServiceProcessInstaller();
            var serviceInstaller = new ServiceInstaller();

            //Set the privillages
            processInstaller.Account = ServiceAccount.LocalSystem;

            serviceInstaller.StartType = ServiceStartMode.Automatic;


            //Add Installers
            this.Installers.Add(processInstaller);
            this.Installers.Add(serviceInstaller);
        }
    }
}
