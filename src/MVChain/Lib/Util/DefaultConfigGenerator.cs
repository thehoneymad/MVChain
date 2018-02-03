using System;
using System.IO;
using System.Net;
using MVChain.Lib.Model;
using MVChain.Lib.Util;
using Newtonsoft.Json;

public class DefaultConfigGenerator
{
    internal static int DefaultPort => 9333;
    internal static IPEndPoint DefaultRemote => new IPEndPoint(IPAddress.Loopback, DefaultPort);
    internal static string DefaultConfigFilePath => Path.GetFullPath("config.json");
    internal static Config DefaultConfig => new Config
    {
        ListenOn = new IPEndPoint(IPAddress.Any, DefaultPort),
        InitialEndpoints = new[] { DefaultRemote },
        KeyPairPath = "<YOUR OWN KEYPAIR>.json",
        GenesisPath = "<GENESIS BLOCK>.bin",
        ShouldMine = true,
    };

    internal static void GenerateDefaultConfig(bool writeToFile = false)
    {
        var defaultConfigJson = JsonConvert.SerializeObject(DefaultConfig, Formatting.Indented);
        Printer.Print(defaultConfigJson);

        if (writeToFile)
        {
            WriteToFile(defaultConfigJson);
        }
    }

    internal static void WriteToFile(string configJsonString)
    {
        if (string.IsNullOrEmpty(configJsonString))
        {
            throw new ArgumentException(nameof(configJsonString));
        }

        using (StreamWriter configFile = new StreamWriter(
            DefaultConfigGenerator.DefaultConfigFilePath, append: false))
        {
            configFile.WriteLine(configJsonString);
        }

        Printer.Print("Config file written at: " + DefaultConfigFilePath);
    }
}