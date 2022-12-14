using PolymorphicDtoApi.Code;
using PolymorphicDtoApi.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PolymorphicDtoApi.Polymorph
{
    public class PolymorphJsonConverter<T> : JsonConverter<PolymorphDto<T>> where T : class, new()
    { 
        private Func<int, Type> getTypeFunc;

        public PolymorphJsonConverter(Func<int, Type> getTypeFunc)
        {
            this.getTypeFunc = getTypeFunc;
        }

        public override PolymorphDto<T>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var readerBack = reader;
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Cannot find StartObject json token");
            }

            if (!reader.Read()
                    || reader.TokenType != JsonTokenType.PropertyName
                    || !string.Equals(nameof(PolymorphDto<T>.TypeDiscriminator), reader.GetString(), StringComparison.InvariantCultureIgnoreCase))
            {
                throw new JsonException($"Cannot find property {nameof(PolymorphDto<T>.TypeDiscriminator)}");
            }

            if (!reader.Read() || reader.TokenType != JsonTokenType.Number)
            {
                throw new JsonException($"Cannot read type discriminator value.");
            }

            var typeDiscriminator = reader.GetInt32();
            var type = getTypeFunc(typeDiscriminator);

            if (!reader.Read()
                    || reader.TokenType != JsonTokenType.PropertyName
                    || !string.Equals(nameof(PolymorphDto<T>.TypeValue), reader.GetString(), StringComparison.InvariantCultureIgnoreCase))
            {
                throw new JsonException($"Cannot find property {nameof(PolymorphDto<T>.TypeValue)}");
            }
            if (!reader.Read() || reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            var optionsCopy = CleanOptions(options);
            var typeValue = JsonSerializer.Deserialize(ref reader, type, optionsCopy) as T;
            if (typeValue == null)
                throw new JsonException("Cannot deserialize type value");

            if (!reader.Read() || reader.TokenType != JsonTokenType.EndObject)
            {
                throw new JsonException("Unexpected json ending. Expected end of json");
            }

            var result = new PolymorphDto<T>(typeDiscriminator, typeValue);
            return result;
        }

        public override void Write(Utf8JsonWriter writer, PolymorphDto<T> value, JsonSerializerOptions options)
        {
            var optionsCopy = CleanOptions(options);
            JsonSerializer.Serialize(writer, value, optionsCopy);
        }

        private static JsonSerializerOptions CleanOptions(JsonSerializerOptions options)
        {
            var optionsCopy = new JsonSerializerOptions(options);
            optionsCopy.Converters.Clear();
            return optionsCopy;
        }
    }
}
