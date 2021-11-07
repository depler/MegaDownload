using CG.Web.MegaApiClient;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MegaDownload.Code
{
    class Program
    {
        static async Task Download(string url, string file)
        {
            Console.WriteLine($"Loading client...");
            var client = new MegaApiClient();
            client.LoginAnonymous();

            var link = new Uri(url);
            var node = client.GetNodeFromLink(link);

            if (string.IsNullOrEmpty(file))
                file = node.Name;

            if (File.Exists(file))
                File.Delete(file);

            var startTime = DateTime.UtcNow;
            var progressTime = DateTime.UtcNow;
            var progressLimit = TimeSpan.FromMilliseconds(500);
            var totalSize = Utils.GetHumanSize(node.Size);

            Console.WriteLine($"Downloading {file}...");

            await client.DownloadFileAsync(link, file, new Progress<double>(value =>
            {
                var dtUtcNow = DateTime.UtcNow;
                if (dtUtcNow - progressTime > progressLimit)
                {
                    progressTime = dtUtcNow;

                    var progressValue = Utils.GetHumanProgress(value);
                    var progressBytes = (long)(node.Size * value / 100);
                    var progressSize = Utils.GetHumanSize(progressBytes);

                    var elapsedTime = (dtUtcNow - startTime).TotalSeconds;
                    var speed = Utils.GetHumanSize((long)(progressBytes / elapsedTime), "/s");

                    Utils.ConsoleClearLine();
                    Console.Write($"{progressValue} ({progressSize} of {totalSize}) - {speed}");
                }
            }));

            client.Logout();

            Utils.ConsoleClearLine();
            Console.WriteLine("Done");
        }

        static async Task Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: MegaDownload [url] <filename>");
                return;
            }

            try
            {
                await Download(args[0], args.Length > 1 ? args[1] : null);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
        }
    }
}
