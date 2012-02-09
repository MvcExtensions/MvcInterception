using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace MvcInterception.Unity
{
    internal static class InterceptionParameterHelper
    {
        public static IActionReturn ToActionInvocation(this IMethodInvocation @this)
        {
            return new ActionReturn(@this.Target, @this.MethodBase, @this.Arguments.ToActionParameters());
        }

        public static IEnumerable<IActionParameter> ToActionParameters(this IParameterCollection @this)
        {
            return Enumerable.Range(0, @this.Count).Select(x => new ActionParameter(x, @this.GetParameterInfo(x), @this[x]));
        }


        public static IActionReturn ApplyMethodReturn(this IActionReturn @this, IMethodReturn res)
        {
            if (res != null)
            {
                if (res.Exception != null)
                {
                    @this.Exception = res.Exception;
                }
                else
                {
                    @this.Value = res.ReturnValue;

                    for (var i = 0; i < res.Outputs.Count; i++)
                        @this.Outputs[i].Value = res.Outputs[i];
                }
                
            }

            return @this;
        }


        public static IMethodReturn ToMethodReturn(this IActionReturn @this, IMethodReturn res, IMethodInvocation original)
        {
            if (res != null)
            {
                if (@this.Exception != null)
                {
                    res.Exception = @this.Exception;
                }
                else
                {
                    res.ReturnValue = @this.Value;

                    for (var i = 0; i < @this.Outputs.Count; i++)
                        res.Outputs[i] = @this.Outputs[i].Value;
                }
                return res;
            }
            else
            {
                return (@this.Exception != null)
                           ? new VirtualMethodReturn(original, @this.Exception)
                           : new VirtualMethodReturn(original, @this.Value, @this.Outputs.Select(x => x.Value).ToArray());
            }
        }
    }
}
