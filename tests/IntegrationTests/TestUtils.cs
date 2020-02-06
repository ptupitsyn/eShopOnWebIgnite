using System;
using Apache.Ignite.Core;
using Apache.Ignite.Core.Discovery.Tcp;
using Apache.Ignite.Core.Discovery.Tcp.Static;
using Microsoft.eShopWeb.Infrastructure.Data.Ignite;

namespace Microsoft.eShopWeb.IntegrationTests
{
    public static class TestUtils
    {
        public static IIgniteAdapter GetIgnite()
        {
            // Fix NullReferenceException handling - see
            // http://apache-ignite-users.70518.x6.nabble.com/SIGSEGV-instead-of-NullReferenceException-when-using-Ignite-NET-td29385.html
            // https://apacheignite-net.readme.io/docs/troubleshooting
            Environment.SetEnvironmentVariable("COMPlus_EnableAlternateStackCheck", "1");
            
            var cfg = new IgniteConfiguration
            {
                DiscoverySpi = new TcpDiscoverySpi
                {
                    SocketTimeout = TimeSpan.FromSeconds(0.3),
                    IpFinder = new TcpDiscoveryStaticIpFinder
                    {
                        Endpoints = new[]{"127.0.0.1:47500"}
                    }
                },
                AutoGenerateIgniteInstanceName = true,
                Localhost = "127.0.0.1"
            };

            var ignite = Ignition.Start(cfg);
            return new IgniteAdapter(ignite);
        }
    }
}