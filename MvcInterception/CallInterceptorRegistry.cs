using System;
using System.Collections;
using System.Linq.Expressions;
using System.Reflection;
using MvcExtensions;

namespace MvcInterception
{
    public abstract class CallInterceptorRegistry : ICallInterceptorRegistry
    {
        protected CallInterceptorRegistry(ContainerAdapter container)
        {
            Container = container;
        }

        public ContainerAdapter Container
        {
            get;
            set;
        }

        public ICallInterceptorRegistry RegisterObjects<TObj>(Expression<Action<TObj>> action, params object[] objs)
        {
            var methodCall = action.Body as MethodCallExpression;

            RegisterMethod(methodCall.Method, methodCall.Object.Type, ConvertInputObjects(objs));

            return this;
        }

        protected abstract IEnumerable ConvertInputObjects(IEnumerable objs);

        protected abstract void RegisterMethod(MethodInfo method, Type type, IEnumerable objects);
    }
}