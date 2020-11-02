using WinService.Model;

namespace WinService.Abstraction
{
    public interface IServiceConfigManager
    {
        /// <summary>
        /// Reads configuration from XML file
        /// </summary>
        /// <returns></returns>
        ServiceConfig Read();

        /// <summary>
        /// Writes configurations to XML file
        /// </summary>
        /// <param name="config"></param>
        void Write(ServiceConfig config);
    }
}
