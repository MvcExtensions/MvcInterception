    public class ConfigureCallInterceptors : ConfigureCallInterceptorsBase, ICallInterceptor
    {
        public ConfigureCallInterceptors(ICallInterceptorRegistry registry) 
            : base(registry)
        {
        }

        protected override void Configure()
        {
            //register all the overridable methods of HomeController for interception - condition x => x.DeclaringType == typeof(HomeController) is here just because in all the over cases - we'll override base class methids
            Registry.RegisterBeforeCancellable<HomeController>(x => x.DeclaringType == typeof(HomeController), BeforeInvocation)
                // or even 
            .RegisterBeforeCancellable<HomeController>(x => x.DeclaringType == typeof(HomeController), x => true)
                // or 
            .RegisterAfter<HomeController>(x => x.DeclaringType == typeof(HomeController), AfterInvocation)
                // or 
            .Register<HomeController>(x => x.DeclaringType == typeof(HomeController), BeforeInvocation, AfterInvocation)
                // or for x.index method directly 
            .RegisterBeforeCancellable<HomeController>(x => x.Index(), BeforeInvocation)
                // or for x.index method directly and use this as call interceptor
            .Register<HomeController>(x => x.Index(), (ICallInterceptor)this);
                //etc etc
        }

        public void AfterInvocation(IActionReturn actionInvocation)
        {
            
        }

        public bool BeforeInvocation(IActionInvocation actionInvocation)
        {
            return true;
        }
    }