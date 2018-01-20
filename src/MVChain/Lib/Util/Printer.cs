namespace MVChain.Lib.Util
{
    using System;
    using MVChain.Lib.Model;
    using Newtonsoft.Json;

    public static class Printer
    {
        public static void Print(IPrintableModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            Console.WriteLine(JsonConvert.SerializeObject(model, Formatting.Indented));
        }

        public static void Print(string data)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                throw new ArgumentException(nameof(data));
            }

            Console.WriteLine(data);
        }
    }
}