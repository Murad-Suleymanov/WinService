# WinService

#      Simple configuration
#      
      WindowsServiceTrigger ctor => WindowsServiceTrigger(string serviceName, string logSourceName, string logName, Action action)

      WindowsServiceTrigger wc = 
                  new WindowsServiceTrigger("TestService", "TestServiceSource", "TestServiceLog", () => { Console.WriteLine("Test"); });

          wc.Start();
            
            
#     Configuration with Ninject

   public class NinjectDependencyResolver : NinjectModule
   
        public override void Load()
        {
            Bind<TestClass>().ToSelf();

            Bind<Action>().ToMethod(ctx=>ctx.Kernel.Get<TestClass>().Print);
            Bind<IWindowsServiceTrigger>().To<WindowsServiceTrigger>()
                .WithConstructorArgument("serviceName", "TestService")
                .WithConstructorArgument("logSourceName", "TestServiceSource")
                .WithConstructorArgument("logName", "TestServiceLog")
                .WithConstructorArgument(new ConstructorArgument("action", ctx => ctx.Kernel.Get<Action>()));
        }
        
          public class TestClass //This is example class. You can choose your class and method.
          {
              public void Print()
              {
                  Console.WriteLine("Test");
              }
          }
}      
  
#       ServiceConfiguration.xml
You can change WorkInterval in ServiceConfiguration.xml.

You can change service configuration property.

ServiceConfiguration.xml located in ExecutingAssembly()\\Config folder.

#     Install console projects as Windows Service
1)Open cmd
  User>"C:\Windows\Microsoft.NET\Framework\v4.0.30319\installutil.exe" /ServiceName=MyConsoleServiceName /DisplayName="MyConsoleServiceName" "...\myConsoleService.exe" 
  ---- Of course can be different version of installutil.exe 
  

#
#
#
#
#
#
#
#
#
#
#
#
