using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Text;

namespace MegaDownload.Code;

public class Config
{
    public readonly string Email;
    public readonly string Password;
    public readonly string MfaKey;

    public Config(string path)
    {
        var json = LoadJson(Path.IsPathRooted(path) ? path : Path.Combine(AppContext.BaseDirectory, path));

        Email = json.Value<string>("Email");
        Password = json.Value<string>("Password");
        MfaKey = json.Value<string>("MfaKey");
    }

    private static JObject LoadJson(string path)
    {
        using var stream = File.Open(path, FileMode.Open);
        using var reader = new StreamReader(stream, Encoding.UTF8);
        using var jreader = new JsonTextReader(reader);
        return (JObject)JToken.ReadFrom(jreader);
    }
}
