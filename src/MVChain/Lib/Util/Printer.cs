using MVChain.Lib.Model;

namespace MVChain.Lib.Util
{
    using System;
    using Newtonsoft.Json;

    internal static class Printer
    {
        internal static void Print(ISerializable model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            Console.WriteLine(JsonConvert.SerializeObject(model, Formatting.Indented));
        }

        internal static void Print(string data)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                throw new ArgumentException(nameof(data));
            }

            Console.WriteLine(data);
        }
    }
}