using System;
using System.Reflection;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace MvcInterception.Unity
{
    public class MethodMatchingRule : IMatchingRule
    {
        public MethodMatchingRule(MethodBase method)
            : this(x => x.Equals(method))
        {
            
        }

        public MethodMatchingRule(Predicate<MethodBase> method)
        {
            MethodEqChecker = method;
        }


        public static MethodMatchingRule Create(Predicate<MethodBase> methodChecker, MethodBase method)
        {
            if (methodChecker != null)
                return new MethodMatchingRule(methodChecker);
            if (method != null)
                return new MethodMatchingRule(method);

            return null;
        }

        public Predicate<MethodBase> MethodEqChecker
        {
            get;
            set;
        }

        public bool Matches(MethodBase member)
        {
            return MethodEqChecker(member);
        }
    }
}