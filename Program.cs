using System;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Cloud.InstrumentationFramework;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SampleIfxApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            InitializeIfx();
            //InitializeLocalIfx();
        }

        private static void InitializeLocalIfx()
        {
            IfxInitializer.IfxInitialize("saianu", "unifiedtestmetrics", "SAI-WORK-PC");
        }

        private static void InitializeIfx()
        {
            IPAddress[] addresses = Dns.GetHostAddresses(Environment.MachineName);
            string ipAddress = null;
            foreach (var addr in addresses)
            {
                if (addr.ToString() == "127.0.0.1")
                {
                    continue;
                }
                else if (addr.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    ipAddress = addr.ToString();
                    break;
                }
            }
            IfxInitializer.IfxInitialize("gua", "guatest", ipAddress);
        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
