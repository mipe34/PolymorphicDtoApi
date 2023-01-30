using PolymorphicDtoApi.Code;
using PolymorphicDtoApi.Models;
using PolymorphicDtoApi.Models.Warrior;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PolymorphicDtoApi.Polymorph
{
    public class PolymorphJsonConverter<T> : JsonConverter<PolymorphDto<T>> where T : class, new()
    {
        ITypeDiscriminator typeDiscriminator;

        public PolymorphJsonConverter(ITypeDiscriminator typeDiscriminator)
        {
            this.typeDiscriminator = typeDiscriminator;
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

            var typeDiscriminatorValue = reader.GetInt32();
            var type = typeDiscriminator.GetType(typeDiscriminatorValue);

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

            var result = new PolymorphDto<T>(typeDiscriminatorValue, typeValue);
            return result;
        }

        public override void Write(Utf8JsonWriter writer, PolymorphDto<T> value, JsonSerializerOptions options)
        {
            var optionsCopy = CleanOptions(options);

            writer.WriteStartObject();

            writer.WriteNumber("TypeDiscriminator", value.TypeDiscriminator);

            writer.WritePropertyName("TypeValue");
            JsonSerializer.Serialize(writer, value.TypeValue, value.TypeValue.GetType(), optionsCopy);

            writer.WriteEndObject();
        }

        public override bool CanConvert(Type typeToConvert)
        {
            var canConvert = typeToConvert.IsAssignableTo(typeof(PolymorphDto<T>));
            return canConvert;
        }

        private static JsonSerializerOptions CleanOptions(JsonSerializerOptions options)
        {
            var optionsCopy = new JsonSerializerOptions(options);
            var thisConverter = optionsCopy.Converters.Single(x=> x.CanConvert(typeof(PolymorphDto<T>)));
            optionsCopy.Converters.Remove(thisConverter);
            return optionsCopy;
        }
    }
}
