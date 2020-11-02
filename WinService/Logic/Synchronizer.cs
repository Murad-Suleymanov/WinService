using System;
using WinService.Abstraction;

namespace WinService.Logic
{
    public class Synchronizer : ISynchronizer
    {
        Action<string> ISynchronizer.Log { get; set; }
        Action<Exception> ISynchronizer.LogException { get; set; }
        Action ISynchronizer.RefreshDependencies { get; set; }
        Func<Type, object> ISynchronizer.GetService { get; set; }
    }
}
