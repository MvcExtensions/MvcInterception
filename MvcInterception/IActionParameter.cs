using System.Reflection;

namespace MvcInterception
{
    public interface IActionParameter
    {
        int Index { get; }
        string Name { get; }
        ParameterInfo ParameterInfo { get; }
        object Value { get; set; }
    }
}
