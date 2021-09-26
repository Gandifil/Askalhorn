using System.Collections.Generic;
using Newtonsoft.Json;

namespace Askalhorn.Text
{
    public enum GrammaticalCase
    {
        Nominative,
        Genitive,
        Dative,
        Accusative,
        Instrumental,
        Prepositional,
    }
    
    [JsonConverter(typeof(TextEntityConverter))]
    public class TextEntity
    {
        public string Nominative => Words[GrammaticalCase.Nominative.ToString()];
        public Dictionary<string, string> Words { get; set; }
    }
}