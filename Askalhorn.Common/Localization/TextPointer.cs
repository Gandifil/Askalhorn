using System;
using Newtonsoft.Json;

namespace Askalhorn.Common.Localization
{
    public class TextPointerConverter : JsonConverter<TextPointer>
    {
        private const char SEPARATOR = ':';
        
        public override void WriteJson(JsonWriter writer, TextPointer? value, JsonSerializer serializer)
        {
            writer.WriteValue(value.Name + SEPARATOR + value.Index);
        }

        public override TextPointer? ReadJson(JsonReader reader, Type objectType, TextPointer? existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            var line= (string)reader.Value;

            var name = line.Substring(0, line.IndexOf(SEPARATOR));
            var index = line.Substring(line.IndexOf(SEPARATOR)+ 1);

            return new TextPointer(name, Convert.ToUInt32(index));
        }
    }
    
    [JsonConverter(typeof(TextPointerConverter))]
    public class TextPointer
    {
        public readonly string Name;

        public readonly uint Index;

        [JsonConstructor]
        public TextPointer(string name, uint index)
        {
            Name = name;
            Index = index;
        }

        public override string ToString()
        {
            var filePath = $"texts/{Name}";
            var lines = Storage.Content.Load<TextFile>(filePath);
            return lines.Lines[Index];
        }
    }
}