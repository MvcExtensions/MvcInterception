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

        public ICallInterceptorRegistry RegisterInterceptors<TObj>(Expression<Action<TObj>> action, params object[] objs)
        {
            var methodCall = action.Body as MethodCallExpression;

            RegisterMethod<TObj>(null, methodCall.Method, ConvertInputObjects(objs));

            return this;
        }

        public ICallInterceptorRegistry RegisterInterceptors<TObj>(Predicate<MethodBase> methodPred, params object[] objs)
        {
            RegisterMethod<TObj>(methodPred, null, ConvertInputObjects(objs));

            return this;
        }

        protected abstract IEnumerable ConvertInputObjects(IEnumerable objs);

        protected abstract void RegisterMethod<TObj>(Predicate<MethodBase> methodPred, MethodBase method, IEnumerable objects);
    }
}