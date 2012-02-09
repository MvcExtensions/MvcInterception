using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using MvcExtensions;
using MvcExtensions.Unity;

namespace MvcInterception.Unity
{
    public class InterceptableUnityBootstrapper : UnityBootstrapper
    {
        public InterceptableUnityBootstrapper(IBuildManager buildManager, IBootstrapperTasksRegistry bootstrapperTasks, IPerRequestTasksRegistry perRequestTasks)
            : base(buildManager, bootstrapperTasks, perRequestTasks)
        {
        }

        protected override ContainerAdapter CreateAdapter()
        {
            var res = (UnityAdapter)base.CreateAdapter();

            res.Container.AddNewExtension<Interception>();
            res.RegisterAsTransient<ICallInterceptorRegistry, UnityCallInterceptorRegistry>();

            return res;
        }
    }
}
