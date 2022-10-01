using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace HttpServer
{
    public class HttpServer
    {
        public class MyHttpServer
        {
            private HttpListener listener;

            public MyHttpServer(string[] prefixes)
            {
                if (!HttpListener.IsSupported)
                {
                    throw new Exception("HttpListener is not supported");

                }

                listener = new HttpListener();

                foreach (string prefix in prefixes) listener.Prefixes.Add(prefix);

            }

            public async Task Start()
            {
                listener.Start();
                Console.WriteLine("http Server Started");


                do
                {

                    Console.WriteLine(DateTime.Now.ToLongTimeString() + "waiting a client connect");
                    var context = await listener.GetContextAsync();
                    await ProcessRequest(context);

                    Console.WriteLine(DateTime.Now.ToLongTimeString() + " client connected");
                }
                while (listener.IsListening);
            }

            async Task ProcessRequest(HttpListenerContext context)
            {
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;

                Console.WriteLine($"{request.HttpMethod} {request.RawUrl} {request.Url.AbsolutePath}");

                var outputStream = response.OutputStream;

                switch (request.Url.AbsolutePath)
                {
                    //http://localhost:8888/google

                    case "/google":
                        {
                            // FileStream fstream = File.OpenRead(@"C:\Users\SURFACE\source\repos\HttpServer\HttpServer\google\google.html");
                            // byte[] buffer = new byte[fstream.Length];

                            var fileName = "index.html";
                            string google = File.ReadAllText(fileName);

                            byte[] buffer = Encoding.UTF8.GetBytes(google);
                            response.ContentLength64 = buffer.Length;
                            await outputStream.WriteAsync(buffer, 0, buffer.Length);
                        }
                        break;


                }

                outputStream.Close();

            }

        }

    }
}

