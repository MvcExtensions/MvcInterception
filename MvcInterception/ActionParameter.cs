using System.Reflection;

namespace MvcInterception
{
    public class ActionParameter : IActionParameter
    {
        public ActionParameter(int index, ParameterInfo parameterInfo, object value)
        {
            Index = index;
            ParameterInfo = parameterInfo;
            Value = value;
        }

        public int Index{ get; private set; }
        public string Name { get { return ParameterInfo.Name; } }
        public ParameterInfo ParameterInfo { get; private set; }
        public object Value { get; set; }
    }
}
