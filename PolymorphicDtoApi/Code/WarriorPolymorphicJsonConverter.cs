using PolymorphicDtoApi.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PolymorphicDtoApi.Code
{
    public class WarriorPolymorphicJsonConverter : JsonConverter<BaseWarriorDto>
    {
        private static readonly Dictionary<WarriorTypeEnum, Type> typeDiscriminationDictionary = new Dictionary<WarriorTypeEnum, Type>()
        {
            { WarriorTypeEnum.Peasant, typeof(PeasantDto)},
            { WarriorTypeEnum.Samurai, typeof(SamuraiDto)},
            { WarriorTypeEnum.Ninja, typeof(NinjaDto)},
        };

        public override BaseWarriorDto? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var readerBack = reader;
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Cannot find StartObject json token");
            }

            if (!reader.Read()
                    || reader.TokenType != JsonTokenType.PropertyName
                    || !string.Equals(nameof(BaseWarriorDto.TypeDiscriminator), reader.GetString(), StringComparison.InvariantCultureIgnoreCase))
            {
                throw new JsonException($"Cannot find property {nameof(BaseWarriorDto.TypeDiscriminator)}");
            }

            if (!reader.Read() || reader.TokenType != JsonTokenType.Number)
            {
                throw new JsonException();
            }

            var typeDiscriminator = (WarriorTypeEnum)reader.GetInt32();
            var type = typeDiscriminationDictionary[typeDiscriminator];

            while (reader.Read()) ;

            var result = JsonSerializer.Deserialize(ref readerBack, type) as BaseWarriorDto;
            return result;
        }

        public override void Write(Utf8JsonWriter writer, BaseWarriorDto value, JsonSerializerOptions options)
        {
            var optionsCopy = new JsonSerializerOptions(options);
            optionsCopy.Converters.Clear();
            JsonSerializer.Serialize(writer, value, optionsCopy);
        }
    }
}
