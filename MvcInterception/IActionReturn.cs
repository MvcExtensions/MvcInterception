using System;
using System.Collections.Generic;

namespace MvcInterception
{
    public interface IActionReturn : IActionInvocation
    {
        IList<IActionParameter> Outputs { get; }
        Exception Exception { get; set; }
        object Value { get; set; }
    }
}
