using System;

namespace MvcInterception
{
    public class DelegatedCallInterceptor : ICallInterceptor
    {
        private readonly Predicate<IActionInvocation> doBeforeInvocation;
        private readonly Action<IActionReturn> doAfterInvocation;

        public DelegatedCallInterceptor(Predicate<IActionInvocation> doBeforeInvocation, Action<IActionReturn> doAfterInvocation)
        {
            this.doBeforeInvocation = doBeforeInvocation;
            this.doAfterInvocation = doAfterInvocation;
        }

        public bool BeforeInvocation(IActionInvocation actionInvocation)
        {
            if (doBeforeInvocation != null)
                return doBeforeInvocation(actionInvocation);

            return true;
        }

        public void AfterInvocation(IActionReturn actionReturn)
        {
            if (doAfterInvocation != null)
                doAfterInvocation(actionReturn);
        }
    }
}
