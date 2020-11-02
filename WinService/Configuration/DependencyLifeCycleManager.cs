namespace WinService.Configuration
{
    public class DependencyLifeCycleManager
    {
        public class ScopeObject { }

        static DependencyLifeCycleManager()
        {
            if (Current == null)
                Current = new ScopeObject();
        }

        public static ScopeObject Current { get; set; }

        public static void Renew()
        {
            if (Current != null)
                Current = null;

            Current = new ScopeObject();
        }
    }
}
