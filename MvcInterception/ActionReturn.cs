using System;
using System.Collections.Generic;
using System.Reflection;

namespace MvcInterception
{
    public class ActionReturn : IActionReturn
    {
        public ActionReturn(object target, MethodBase methodBase, IEnumerable<IActionParameter> inputs)
        {
            Target = target;
            MethodBase = methodBase;
            Inputs = new List<IActionParameter>(inputs);
            Outputs = new List<IActionParameter>();
            var mi = methodBase as MethodInfo;

            if (mi != null && mi.ReturnType.IsValueType && mi.ReturnType != typeof(void))
                Value = Activator.CreateInstance(mi.ReturnType);
        }

        public IActionReturn SetReturnData(IEnumerable<IActionParameter> outputs, Exception exc, object value)
        {
            Outputs = new List<IActionParameter>(outputs);
            Exception = exc;
            Value = value;

            return this;
        }

        public object Target { get; set; }
        public string Name { get { return MethodBase.Name; } }
        public MethodBase MethodBase { get; private set; }
        public IList<IActionParameter> Inputs { get; private set; }
        public IList<IActionParameter> Outputs { get; private set; }
        public Exception Exception { get; set; }
        public object Value { get; set; }
    }
}
