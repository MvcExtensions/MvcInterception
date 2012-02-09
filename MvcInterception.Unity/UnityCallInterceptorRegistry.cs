using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using MvcExtensions;
using MvcExtensions.Unity;

namespace MvcInterception.Unity
{
    public class UnityCallInterceptorRegistry : CallInterceptorRegistry 
    {
        private static readonly Dictionary<string, PolicyDefinition> cachePolicy = new Dictionary<string, PolicyDefinition>();
        private static readonly Dictionary<ICallInterceptor, ICallHandler> cacheHandler = new Dictionary<ICallInterceptor, ICallHandler>();

        private static readonly ITypeInterceptor virtualMethodInterceptor = new VirtualMethodInterceptor();
        private static readonly IInstanceInterceptor interfaceInterceptor = new InterfaceInterceptor();
        private static readonly IInstanceInterceptor transparentProxyInterceptor = new TransparentProxyInterceptor();
        
        public UnityCallInterceptorRegistry(ContainerAdapter container)
            : base(container)
        {
        }

        protected Interception InterceptionExtension
        {
            get
            {
                return ((UnityAdapter)Container).Container.Configure<Interception>();
            }
        }

        protected override void RegisterMethod(MethodInfo method, Type type, IEnumerable objects)
        {
            var policyName = type.FullName + "." + method.Name + ", " + type.Assembly.FullName;

            PolicyDefinition policy;

            if (!cachePolicy.TryGetValue(policyName, out policy))
            {
                Interception interceptionExtension = null;

                if (transparentProxyInterceptor.CanIntercept(type))
                    interceptionExtension = InterceptionExtension.SetInterceptorFor(type, transparentProxyInterceptor);
                else if (interfaceInterceptor.CanIntercept(type))
                    interceptionExtension = InterceptionExtension.SetInterceptorFor(type, interfaceInterceptor);
                else
                    interceptionExtension = InterceptionExtension.SetInterceptorFor(type, virtualMethodInterceptor);

                policy = interceptionExtension.AddPolicy(policyName).AddMatchingRule(new MethodMatchingRule(method));

                cachePolicy.Add(policyName, policy);
            }

            if (objects != null)
            {
                foreach (var typeh in objects)
                    policy.AddCallHandler((ICallHandler) typeh);
            }
        }


        protected override IEnumerable ConvertInputObjects(IEnumerable xobjs)
        {
            if (xobjs == null)
                return null;

            return xobjs.Cast<object>().Where(xobj => xobj != null)
                .Select(x => x is Type ? Container.GetService((Type)x) as ICallInterceptor : x as ICallInterceptor)
                .Where(x => x != null).Select( GetOrCreateHandler );
        }

        private ICallHandler GetOrCreateHandler(ICallInterceptor callInterceptor)
        {
            ICallHandler result;

            if (!cacheHandler.TryGetValue(callInterceptor, out result))
            {
                result = new InterceptedCallHandler(callInterceptor);
                cacheHandler.Add(callInterceptor, result);
            }

            return result;
        }
    }
}