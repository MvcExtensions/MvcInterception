using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MvcInterception
{
    public static class CallInterceptorRegistryMethodExtensions
    {
        public static ICallInterceptorRegistry Register<TObj>(this ICallInterceptorRegistry @this, Predicate<MethodBase> methodPred, params ICallInterceptor[] objs)
        {
            return @this.Register<TObj>(methodPred, (IEnumerable<ICallInterceptor>)objs);
        }

        public static ICallInterceptorRegistry Register<TObj>(this ICallInterceptorRegistry @this, Predicate<MethodBase> methodPred, IEnumerable<ICallInterceptor> objs)
        {
            return @this.RegisterInterceptors<TObj>(methodPred, objs.Cast<object>().ToArray());
        }

        public static ICallInterceptorRegistry Register<TObj, T1>(this ICallInterceptorRegistry @this, Predicate<MethodBase> methodPred)
            where T1: ICallInterceptor, new()
        {
            return @this.RegisterInterceptors<TObj>(methodPred, typeof(T1));
        }

        public static ICallInterceptorRegistry Register<TObj, T1, T2>(this ICallInterceptorRegistry @this, Predicate<MethodBase> methodPred)
            where T1 : ICallInterceptor, new()
            where T2 : ICallInterceptor, new()
        {
            return @this.RegisterInterceptors<TObj>(methodPred, typeof(T1), typeof(T2));
        }

        public static ICallInterceptorRegistry Register<TObj, T1, T2, T3>(this ICallInterceptorRegistry @this, Predicate<MethodBase> methodPred)
            where T1 : ICallInterceptor, new()
            where T2 : ICallInterceptor, new()
            where T3 : ICallInterceptor, new()
        {
            return @this.RegisterInterceptors<TObj>(methodPred, typeof(T1), typeof(T2), typeof(T3));
        }

        public static ICallInterceptorRegistry Register<TObj, T1, T2, T3, T4>(this ICallInterceptorRegistry @this, Predicate<MethodBase> methodPred)
            where T1 : ICallInterceptor, new()
            where T2 : ICallInterceptor, new()
            where T3 : ICallInterceptor, new()
            where T4 : ICallInterceptor, new()
        {
            return @this.RegisterInterceptors<TObj>(methodPred, typeof(T1), typeof(T2), typeof(T3), typeof(T4));
        }

        public static ICallInterceptorRegistry Register<TObj>(this ICallInterceptorRegistry @this, Predicate<MethodBase> methodPred, Predicate<IActionInvocation> before, Action<IActionReturn> after)
        {
            return @this.RegisterInterceptors<TObj>(methodPred, new DelegatedCallInterceptor(before, after));
        }

        public static ICallInterceptorRegistry RegisterBefore<TObj>(this ICallInterceptorRegistry @this, Predicate<MethodBase> methodPred, Action<IActionInvocation> before)
        {
            return @this.Register<TObj>(methodPred, x => { before(x); return true; }, null);
        }

        public static ICallInterceptorRegistry RegisterBeforeCancellable<TObj>(this ICallInterceptorRegistry @this, Predicate<MethodBase> methodPred, Predicate<IActionInvocation> before)
        {
            return @this.Register<TObj>(methodPred, before, null);
        }

        public static ICallInterceptorRegistry RegisterAfter<TObj>(this ICallInterceptorRegistry @this, Predicate<MethodBase> methodPred, Action<IActionReturn> after)
        {
            return @this.Register<TObj>(methodPred, null, after);
        }
    }
}
