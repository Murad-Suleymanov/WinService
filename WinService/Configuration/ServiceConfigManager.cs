using System;
using System.IO;
using System.Xml.Serialization;
using WinService.Abstraction;
using WinService.Model;

namespace WinService.Configuration
{
    public class ServiceConfigManager : IServiceConfigManager
    {
        private readonly string fileName = "ServiceConfiguration.xml";
        private readonly string filePath;

        public ServiceConfigManager()
        {
            filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
        }

        ServiceConfig IServiceConfigManager.Read()
        {
            ServiceConfig config = null;

            var configurationFile = new FileInfo(filePath);

            if (configurationFile.Exists)
            {
                var serializer = new XmlSerializer(typeof(ServiceConfig));
                config = serializer.Deserialize(configurationFile.OpenRead()) as ServiceConfig;
            }

            return config;
        }

        void IServiceConfigManager.Write(ServiceConfig config)
        {
            if (config != null)
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    var serializer = new XmlSerializer(typeof(ServiceConfig));
                    serializer.Serialize(writer, config);
                }
            }
        }
    }
}
