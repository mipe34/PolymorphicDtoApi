using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Linq;

namespace PolymorphicDtoApi.Code.Xml
{
    public class XElementJsonConverter : JsonConverter<XElement>
    {
        public override XElement? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();

            if (value == null) throw new ArgumentNullException("value");

            try
            {
                return XElement.Parse(value);
            }
            catch (XmlException)
            {
                // TODO log error
                throw;
            }
        }

        public override void Write(Utf8JsonWriter writer, XElement value, JsonSerializerOptions options)
        {
            value.Name = "root";
            writer.WriteStringValue(value.ToString());
        }
    }
}
