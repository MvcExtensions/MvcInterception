using MvcExtensions;

namespace MvcInterception
{
    [DependsOn(typeof(RegisterCallInterceptors))]
    public abstract class ConfigureCallInterceptorsBase : BootstrapperTask
    {
        /// <summary>
        /// Gets the filter registry.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// The filter registry.
        /// </value>
        protected ICallInterceptorRegistry Registry { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MvcInterception.ConfigureCallInterceptorsBase"/> class.
        /// </summary>
        /// <param name="registry">The registry.</param>
        protected ConfigureCallInterceptorsBase(ICallInterceptorRegistry registry)
        {
            Invariant.IsNotNull(registry, "registry");
            Registry = registry;
        }

        /// <summary>
        /// Executes the task.
        /// </summary>
        /// <returns/>
        public override TaskContinuation Execute()
        {
            Configure();
            return TaskContinuation.Continue;
        }

        /// <summary>
        /// Configures the filters.
        /// </summary>
        protected abstract void Configure();
    }
}
