﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MvcInterception
{
    public static class CallInterceptorRegistryExpressionExtensions
    {
        public static ICallInterceptorRegistry Register<TObj>(this ICallInterceptorRegistry @this, Expression<Action<TObj>> action, params ICallInterceptor[] objs)
        {
            return @this.Register(action, (IEnumerable<ICallInterceptor>)objs);
        }

        public static ICallInterceptorRegistry Register<TObj>(this ICallInterceptorRegistry @this, Expression<Action<TObj>> action, IEnumerable<ICallInterceptor> objs)
        {
            return @this.RegisterInterceptors(action, objs.Cast<object>().ToArray());
        }

        public static ICallInterceptorRegistry Register<TObj, T1>(this ICallInterceptorRegistry @this, Expression<Action<TObj>> action)
            where T1: ICallInterceptor, new()
        {
            return @this.RegisterInterceptors(action, typeof(T1));
        }

        public static ICallInterceptorRegistry Register<TObj, T1, T2>(this ICallInterceptorRegistry @this, Expression<Action<TObj>> action)
            where T1 : ICallInterceptor, new()
            where T2 : ICallInterceptor, new()
        {
            return @this.RegisterInterceptors(action, typeof(T1), typeof(T2));
        }

        public static ICallInterceptorRegistry Register<TObj, T1, T2, T3>(this ICallInterceptorRegistry @this, Expression<Action<TObj>> action)
            where T1 : ICallInterceptor, new()
            where T2 : ICallInterceptor, new()
            where T3 : ICallInterceptor, new()
        {
            return @this.RegisterInterceptors(action, typeof(T1), typeof(T2), typeof(T3));
        }

        public static ICallInterceptorRegistry Register<TObj, T1, T2, T3, T4>(this ICallInterceptorRegistry @this, Expression<Action<TObj>> action)
            where T1 : ICallInterceptor, new()
            where T2 : ICallInterceptor, new()
            where T3 : ICallInterceptor, new()
            where T4 : ICallInterceptor, new()
        {
            return @this.RegisterInterceptors(action, typeof(T1), typeof(T2), typeof(T3), typeof(T4));
        }

        public static ICallInterceptorRegistry Register<TObj>(this ICallInterceptorRegistry @this, Expression<Action<TObj>> action, Predicate<IActionInvocation> before, Action<IActionReturn> after)
        {
            return @this.RegisterInterceptors(action, new DelegatedCallInterceptor(before, after));
        }

        public static ICallInterceptorRegistry RegisterBefore<TObj>(this ICallInterceptorRegistry @this, Expression<Action<TObj>> action, Action<IActionInvocation> before)
        {
            return @this.Register(action, x => { before(x); return true; }, null);
        }

        public static ICallInterceptorRegistry RegisterBeforeCancellable<TObj>(this ICallInterceptorRegistry @this, Expression<Action<TObj>> action, Predicate<IActionInvocation> before)
        {
            return @this.Register(action, before, null);
        }

        public static ICallInterceptorRegistry RegisterAfter<TObj>(this ICallInterceptorRegistry @this, Expression<Action<TObj>> action, Action<IActionReturn> after)
        {
            return @this.Register(action, null, after);
        }
    }
}
