using System;

namespace WinService.Abstraction
{
    public interface ISynchronizer
    {
        /// <summary>
        /// Logs information
        /// </summary>
        Action<string> Log { get; set; }

        /// <summary>
        /// Logs exception
        /// </summary>
        Action<Exception> LogException { get; set; }

        /// <summary>
        /// Callback for refreshing depencencies
        /// </summary>
        Action RefreshDependencies { get; set; }

        /// <summary>
        /// Callback for dependency injection
        /// </summary>
        Func<Type, object> GetService { get; set; }
    }
}
