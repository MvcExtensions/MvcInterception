using Microsoft.Practices.Unity.InterceptionExtension;

namespace MvcInterception.Unity
{
    public class InterceptedCallHandler<T> : InterceptedCallHandler
        where T : ICallInterceptor, new()
    {
        private ICallInterceptor invocationHandler;

        public InterceptedCallHandler()
            : base (null)
        {
        }

        public override ICallInterceptor InvocationHandler
        {
            get
            {
                return invocationHandler ?? (invocationHandler = new T());
            }
            set { }
        }
    }

    public class InterceptedCallHandler : ICallHandler
    {
        public virtual ICallInterceptor InvocationHandler
        {
            get;
            set;
        }

        public InterceptedCallHandler(ICallInterceptor handler)
        {
            InvocationHandler = handler;
        }


        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            var inp = input.ToActionInvocation();
            IMethodReturn res = null;

            if (InvocationHandler.BeforeInvocation(inp))
            {
                res = getNext()(input, getNext);

                InvocationHandler.AfterInvocation(inp.ApplyMethodReturn(res));
            }

            return inp.ToMethodReturn(res, input);
        }
 
        public int Order { get; set; }
    }
}
