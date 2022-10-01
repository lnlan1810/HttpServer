using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using static HttpServer.HttpServer;

namespace HttpServer
{
    public class Program
    {
       

        static async Task Main(string[] args)
        {

            var server = new MyHttpServer(new string[] { "http://localhost:8888/" });
            await server.Start();
        }


    }
}
