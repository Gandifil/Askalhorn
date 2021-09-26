using System;
using Askalhorn.Common;
using Newtonsoft.Json;

namespace Askalhorn.Text
{
    [JsonConverter(typeof(TextPointerConverter))]
    public class TextPointer
    {
        public string Name { get; }
        
        public string Index { get; }
        
        public GrammaticalCase? GrammaticalCase { get; set; } = null;

        [JsonConstructor]
        public TextPointer(string name, string index)
        {
            Name = name;
            Index = index;
        }
        
        public string Extract(string color = null)
        {
            var filePath = $"texts/{Name}";
            var texts = Storage.Content.Load<TextFile>(filePath);
            var gcase = GrammaticalCase ?? Text.GrammaticalCase.Nominative;
            var line = texts[Index].Words[gcase.ToString()];
            if (!string.IsNullOrEmpty(color))
                line = line.WithColor(color);
            return line;
        }
        
        public override string ToString()
        {
            return Extract();
        }
    }
}