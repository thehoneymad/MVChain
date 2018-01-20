namespace MVChain.Immutables
{
    using System;
    using global::MVChain.Immutables.Util;
    using Newtonsoft.Json;

    public class ReadonlyBytesConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) =>
            objectType == typeof(byte[]);

        public override object ReadJson(
            JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            return new ReadonlyBytes(HexConverter.ToBytes(
                serializer.Deserialize<string>(reader) ?? ""));
        }

        public override void WriteJson(
            JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(
                HexConverter.FromBytes(((ReadonlyBytes)value).bytes));
        }
    }
}