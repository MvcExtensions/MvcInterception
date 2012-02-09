using System.Collections.Generic;
using System.Reflection;

namespace MvcInterception
{
    public interface IActionInvocation
    {
        object Target { get; }
        string Name { get; }
        MethodBase MethodBase { get; }
        IList<IActionParameter> Inputs { get; }
    }
}
