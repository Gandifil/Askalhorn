using Askalhorn.Common;
using Newtonsoft.Json;

namespace Askalhorn.Text
{
    [JsonConverter(typeof(TextPointerConverter))]
    public class TextPointer
    {
        public readonly string Name;

        public readonly string Index;

        [JsonConstructor]
        public TextPointer(string name, string index)
        {
            Name = name;
            Index = index;
        }

        public override string ToString()
        {
            var filePath = $"texts/{Name}";
            var texts = Storage.Content.Load<TextFile>(filePath);
            return texts[Index];
        }
    }
}