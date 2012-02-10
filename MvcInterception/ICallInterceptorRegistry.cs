using System;
using System.Linq.Expressions;
using System.Reflection;
using MvcExtensions;

namespace MvcInterception
{
    public interface ICallInterceptorRegistry
    {
        ContainerAdapter Container
        {
            get;
            set;
        }

        ICallInterceptorRegistry RegisterInterceptors<TObj>(Expression<Action<TObj>> action, params object[] objs);

        ICallInterceptorRegistry RegisterInterceptors<TObj>(Predicate<MethodBase> methodPred, params object[] objs);
    }
}
