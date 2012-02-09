using System;
using System.Linq.Expressions;
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

        ICallInterceptorRegistry RegisterObjects<TObj>(Expression<Action<TObj>> action, params object[] objs);
    }
}
