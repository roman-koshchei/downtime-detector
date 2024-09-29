using System.Threading.Channels;

if (!int.TryParse(args.FirstOrDefault(), out int period)) period = 10;

var url = "https://pocketbase.cookingweb.dev/_/";
var start = DateTime.Now;

HttpClient client = new()
{
    Timeout = TimeSpan.FromMilliseconds(1500)
};

Channel<bool> channel = Channel.CreateUnbounded<bool>();

var pingLoop = Task.Run(() =>
{
    while (true)
    {
        var now = DateTime.Now;
        if ((now - start).TotalMilliseconds > period)
        {
            _ = Ping(client, url, channel.Writer);
            start = now;
        }
    }
});

var printLoop = Task.Factory.StartNew(async () =>
{
    while (true)
    {
        var up = await channel.Reader.ReadAsync();
        Console.ForegroundColor = up ? ConsoleColor.Green : ConsoleColor.Red;
        Console.Write("|");
    }
});

Task.WaitAll(pingLoop, printLoop);

Console.ResetColor();

static async Task Ping(HttpClient client, string url, ChannelWriter<bool> channel)
{
    try
    {
        var res = await client.GetAsync(url);
        channel.TryWrite(res.IsSuccessStatusCode);
    }
    catch
    {
        channel.TryWrite(false);
    }
}