﻿
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSB01SelfHost
{
    class Program
    {
        static void Main( string[] args )
        {
            var cfg = new BusConfiguration();
            cfg.EnableInstallers();

            cfg.UsePersistence<InMemoryPersistence>();
            cfg.UseSerialization<JsonSerializer>();
            cfg.UseTransport<RabbitMQTransport>()
                .ConnectionString("host=localhost");

            cfg.Conventions()
                .DefiningCommandsAs( t => t.Namespace != null && t.Namespace.EndsWith( ".Commands" ) )
                .DefiningEventsAs( t => t.Namespace != null && t.Namespace.EndsWith( ".Events" ) );

            using ( var bus = Bus.Create( cfg ).Start() )
            {
                Console.Read();
            }
        }
    }
}
