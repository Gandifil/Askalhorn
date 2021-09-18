using Newtonsoft.Json;

namespace Askalhorn.Map.Actions
{
    public interface IActionable
    {
        [JsonIgnore]
        IAction Action { get; }
    }
}