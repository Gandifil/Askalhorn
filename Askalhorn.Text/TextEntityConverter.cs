using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Askalhorn.Text
{
    public class TextEntityConverter: JsonConverter<TextEntity>
    {
        private const char SEPARATOR = ':';
        
        public override void WriteJson(JsonWriter writer, TextEntity? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override TextEntity? ReadJson(JsonReader reader, Type objectType, TextEntity? existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.String)
                return new TextEntity()
                {
                    Words = new Dictionary<string, string>
                    {
                        {GrammaticalCase.Nominative.ToString(), (string) reader.Value ?? "unknown"}
                    }
                };

            if (reader.TokenType == JsonToken.StartObject)
            {
                var dictionary = new Dictionary<string, string>();
                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.EndObject)
                    {
                        return new TextEntity()
                        {
                            Words = dictionary,
                        };
                    }

                    if (reader.TokenType != JsonToken.PropertyName)
                    {
                        throw new JsonException("JsonToken was not PropertyName");
                    }

                    var propertyName = reader.Value as string;

                    if (string.IsNullOrWhiteSpace(propertyName))
                    {
                        throw new JsonException("Failed to get property name");
                    }
                    reader.Read();

                    if (reader.TokenType != JsonToken.String)
                    {
                        throw new JsonException("JsonToken was not String");
                    }

                    var propertyValue = reader.Value as string;

                    if (string.IsNullOrWhiteSpace(propertyValue))
                    {
                        throw new JsonException("Failed to get property value");
                    }

                    dictionary.Add(propertyName, propertyValue);
                }
                return new TextEntity()
                {
                    Words = dictionary,
                };
            }

            return null;
        }
    }
}