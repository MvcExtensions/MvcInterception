using System.Reflection;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace MvcInterception.Unity
{
    public class MethodMatchingRule : IMatchingRule
    {
        public MethodMatchingRule()
        {
        }

        public MethodMatchingRule(MethodInfo method)
        {
            Method = method;
        }

        public MethodInfo Method
        {
            get;
            set;
        }

        public bool Matches(MethodBase member)
        {
            return member.Equals(Method);
        }
    }
}