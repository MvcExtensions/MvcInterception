    public class MvcApplication : InterceptableUnityMvcApplication
    {
        public MvcApplication()
        {
            Bootstrapper.BootstrapperTasks
                .Include<RegisterAreas>()
                .Include<RegisterRoutes>()
                .Include<RegisterControllers>()
                .Include<RegisterModelMetadata>()
                .Include<RegisterCallInterceptors>() //use this line to register available call interceptors and 
                .Include<ConfigureCallInterceptors>(); //override ConfigureCallInterceptorsBase to configure items to intercept
        }
    }