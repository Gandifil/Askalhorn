using System.IO;
using Microsoft.Xna.Framework.Content;
using Newtonsoft.Json;

namespace Askalhorn.Common
{
    public class PolymorphJsonReader<T> : ContentTypeReader<T>
    {
        protected override T Read(ContentReader reader, T existingInstance)
        {
            using (StringReader stringReader = new StringReader(reader.ReadString()))
            {
                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                };
                
                return JsonConvert.DeserializeObject<T>(stringReader.ReadToEnd(), settings);
            }
        }
    }
}