using MvcExtensions;

namespace MvcInterception
{
    public class RegisterCallInterceptors : RegistrationTask<ICallInterceptor>
    {
        public RegisterCallInterceptors(ContainerAdapter container)
            : base(container)
        {
        }
    }
}
