using Newtonsoft.Json;
using System;
using System.Collections.Generic;

public class EmptyListToNullConverter<T> : JsonConverter<List<T>>
{
    public override void WriteJson(JsonWriter writer, List<T> value, JsonSerializer serializer)
    {
        if (value == null || value.Count == 0)
        {
            writer.WriteNull();
        }
        else
        {
            serializer.Serialize(writer, value);
        }
    }

    public override List<T> ReadJson(JsonReader reader, Type objectType, List<T> existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
        {
            return null;
        }
        return serializer.Deserialize<List<T>>(reader);
    }
}
