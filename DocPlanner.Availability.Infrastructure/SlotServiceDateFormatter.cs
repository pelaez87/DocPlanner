using System.Text.Json;
using System.Text.Json.Serialization;

namespace DocPlanner.Availability.Infrastructure
{
    internal class SlotServiceDateFormatter : JsonConverter<DateTime>
    {
        private const string Format = "yyyy-MM-dd HH:mm:ss";

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Not needed, just used to send data to the external API
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(Format));
        }
    }
}
