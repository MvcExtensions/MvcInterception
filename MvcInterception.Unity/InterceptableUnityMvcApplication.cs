using MvcExtensions;
using MvcExtensions.Unity;

namespace MvcInterception.Unity
{
    public class InterceptableUnityMvcApplication : UnityMvcApplication
    {

        protected override IBootstrapper CreateBootstrapper()
        {
            return new InterceptableUnityBootstrapper(BuildManagerWrapper.Current, BootstrapperTasksRegistry.Current, PerRequestTasksRegistry.Current);
        }
    }
}
