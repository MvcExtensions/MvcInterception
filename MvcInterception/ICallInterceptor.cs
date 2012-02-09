namespace MvcInterception
{
    public interface ICallInterceptor
    {
        bool BeforeInvocation(IActionInvocation actionInvocation);

        void AfterInvocation(IActionReturn actionReturn);
    }
}
