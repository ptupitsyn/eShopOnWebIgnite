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
                AutoGenerateIgniteInstanceName = true
            };

            var ignite = Ignition.Start(cfg);
            return new IgniteAdapter(ignite);
        }
    }
}