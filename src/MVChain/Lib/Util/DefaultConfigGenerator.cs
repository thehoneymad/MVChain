using System.IO;
using System.Net;
using MVChain.Lib.Model;
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
}