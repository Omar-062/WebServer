using System.Net;
using System.Text.RegularExpressions;

WebHost host = new("http://localhost",8080);
host.Start(); 

class WebHost
{
    private string url;
    private HttpListener listener;

    public WebHost(string prefix,UInt16 port)
    {
        Regex re = new(@"^https?:");
        if (re.IsMatch(prefix))
        {
            this.url = $"{prefix}:{port}/";
        }
        this.listener = new();
        listener.Prefixes.Add(this.url);
    }

    public void Start()
    {
        listener.Start();

        while (true)
        {
            Console.WriteLine("Waiting for request...");
            HttpListenerContext context = listener.GetContext();

            Console.WriteLine("Request received!");
            HttpListenerRequest request = context.Request;

            Console.WriteLine($"Method: {request.HttpMethod}");
            Console.WriteLine($"Path: {request.Url.AbsolutePath}");

            if (request.Url.AbsolutePath.ToLower()=="/first")
            {
                var bytes = File.ReadAllBytes("Views/first.html");
                context.Response.ContentType = "text/html";
                context.Response.OutputStream.Write(bytes, 0, bytes.Length);
            }
        }
    }
}

 
 

