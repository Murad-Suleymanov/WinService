using Ma.LogManager;
using Ninject;
using NProjects.Utility.Core;
using System;
using System.Diagnostics;
using System.Reflection;
using System.ServiceProcess;
using System.Threading.Tasks;
using System.Timers;
using WinService.Abstraction;
using WinService.Configuration;
using WinService.Model;

namespace WinService
{
    class Program
    {
        private static EventLog eventLog;
        private static StandardKernel kernel;
        private static LogManager logger;
        private static Timer timer;
        private static ServiceConfig config;
        private static Action _synchronizerMethod;

        static void Register(string logSourceName, string logName)
        {
            kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            eventLog = LogManager.InitializeEventLog(logSourceName, logName);
            logger = new LogManager(eventLog);
        }

        public static void Main(string serviceName, string logSourceName, string logName, Action action)
        {
            Register(logSourceName, logName);

            if (!Environment.UserInteractive)
            {
                using (var service = new GenericWinService(serviceName))
                {
                    _synchronizerMethod = action;
                    ServiceBase.Run(service);
                }
            }
            else
            {
                Console.WriteLine("Started");

                Start();
#if DEBUG
                Console.WriteLine("Completed. Press enter to continue...");
#else
                Console.WriteLine("Service working in background. Press enter to stop...");
#endif
                Console.ReadLine();

                Stop();
            }
        }


        internal static void Start()
        {
            try
            {
                logger.Log("Started");

                var configManager = kernel.Get<IServiceConfigManager>();
                config = configManager.Read();

                //Initialize and start the timer
                timer = new Timer(15 * 1000);

#if !DEBUG
                timer.Elapsed += OnTimerElapsed;
                timer.Start();
#else
                OnTimerElapsed(timer, null);
#endif
            }
            catch (Exception ex)
            {
                var exDetail = new ExceptionDetails
                {
                    ErrorDate = DateTime.Now,
                    MethodName = "Start",
                    ErrorMessage = ex.GetFullErrorMessage()
                };

                logger.Log(exDetail);
            }
        }

        internal static void Stop()
        {
            try
            {
                DependencyLifeCycleManager.Renew();
                logger.Log("Stopped");
                timer.Elapsed -= OnTimerElapsed;
                timer.Stop();
            }
            catch (Exception ex)
            {
                var exDetail = new ExceptionDetails
                {
                    ErrorDate = DateTime.Now,
                    MethodName = "Stop",
                    ErrorMessage = ex.GetFullErrorMessage()
                };

                logger.Log(exDetail);//
            }
        }

        private static void OnTimerElapsed(object sender, EventArgs args)
        {
            timer.Stop();

            DependencyLifeCycleManager.Renew();

            var synchronizer = kernel.Get<ISynchronizer>();
            synchronizer.Log = logger.Log;
            synchronizer.LogException = LogException;
            synchronizer.RefreshDependencies = DependencyLifeCycleManager.Renew;
            synchronizer.GetService = t => kernel.Get(t);
            Task.Run(() =>
            {
                _synchronizerMethod();
            });

#if !DEBUG
            timer.Interval = TimeSpan.FromMinutes(config.WorkInterval).TotalMilliseconds;
            timer.Start();
#endif
        }

        private static void LogException(Exception ex)
        {
            var exDetail = new ExceptionDetails()
            {
                MethodName = ex.Source,
                ErrorDate = DateTime.Now,
                ErrorMessage = ex.GetFullErrorMessage()
            };

            logger.Log(exDetail);
        }
    }
}

