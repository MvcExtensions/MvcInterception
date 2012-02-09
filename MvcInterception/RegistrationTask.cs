using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using MvcExtensions;

namespace MvcInterception
{
    /// <summary>
    /// Defines a class which is used to register available <seealso cref="T:System.Web.Mvc.IMvcFilter"/>.
    /// 
    /// </summary>
    public class RegistrationTask<T> : IgnorableTypesBootstrapperTask<RegistrationTask<T>, T> 
        where T : class
    {

        public static readonly Type KnownType = typeof(T);
        public static readonly Type KnownAttributeType = typeof(Attribute);

        public static readonly Assembly AspNetMvcAssembly = typeof(Controller).Assembly;
        public static readonly Assembly AspNetMvcExtensionsAssembly = typeof(Bootstrapper).Assembly;
// ReSharper disable InconsistentNaming
        public const string AspNetMvcFutureAssemblyName = "Microsoft.Web.Mvc";
// ReSharper restore InconsistentNaming


        /// <summary>
        /// Gets the container.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// The container.
        /// </value>
        protected ContainerAdapter Container { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MvcExtensions.RegisterFilters"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public RegistrationTask(ContainerAdapter container)
        {
            Invariant.IsNotNull(container, "container");
            Container = container;
        }

        /// <summary>
        /// Filter to be applied to Concrete types list
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected virtual bool MatchTypeFilter(Type type)
        {
            if (KnownType.IsAssignableFrom(type) && !KnownAttributeType.IsAssignableFrom(type) &&
                (type.Assembly != AspNetMvcAssembly && type.Assembly != AspNetMvcExtensionsAssembly) &&
                !type.Assembly.GetName().Name.Equals(AspNetMvcFutureAssemblyName, StringComparison.OrdinalIgnoreCase))
            {
                return IgnoredTypes.All(ignoredType => ignoredType != type);
            }
            return false;
        }

        /// <summary>
        /// Executes the task. Returns continuation of the next task(s) in the chain.
        /// </summary>
        /// <returns/>
        public override TaskContinuation Execute()
        {
            Container.GetService<IBuildManager>().ConcreteTypes.Where(MatchTypeFilter).Each(type => Container.RegisterAsTransient(type));

            return TaskContinuation.Continue;
        }
    }
}
