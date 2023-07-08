using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace WebServer.Classes;

class WebHost
{
    private string url;
    private HttpListener listener;

    public WebHost(string prefix, UInt16 port)
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


            string requestPath = request.Url.AbsolutePath;

/*
            // Способ №1
            if (request.Url.AbsolutePath.ToLower() == "/first")
            {
                var bytes = File.ReadAllBytes("Views/first.html");
                context.Response.ContentType = "text/html";
                context.Response.OutputStream.Write(bytes, 0, bytes.Length);
            }
*/
            if (requestPath.ToString() != "/")
            {
                StringBuilder sb = new("Views");
                sb.Append(requestPath);
                sb.Append(".html");
                requestPath = sb.ToString();
            }

            var files = Directory.GetFiles("Views");

            foreach (var fileName in files)
            {
                string pathWithForwardSlash = fileName;
                string pathWithBackslash = pathWithForwardSlash.Replace("\\", "/");
                if (requestPath == pathWithBackslash)
                {
                    var bytes = File.ReadAllBytes(pathWithBackslash);

                    context.Response.ContentType = "text/html";
                    context.Response.OutputStream.Write(bytes, 0, bytes.Length);
                }
            }
        }
    }
}