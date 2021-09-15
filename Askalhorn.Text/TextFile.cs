using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Askalhorn.Text
{
    public class TextFile
    {
        [JsonExtensionData] 
        public Dictionary<string, JToken> Data { get; set; }

        public string this[string index] => Data[index].ToString();
    }
}