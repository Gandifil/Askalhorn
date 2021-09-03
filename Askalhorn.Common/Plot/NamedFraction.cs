using System.Text.Json.Serialization;

namespace Askalhorn.Common.Plot
{
    public class NamedFraction: IFraction
    {
        public string Name { get; }

        [JsonConstructor]
        public NamedFraction(string name)
        {
            Name = name;
        }
    }
}