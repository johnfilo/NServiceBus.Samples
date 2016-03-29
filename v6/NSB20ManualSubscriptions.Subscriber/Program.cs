﻿using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSB20ManualSubscriptions.Subscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            var cfg = new EndpointConfiguration(typeof(Program).Namespace);

            cfg.UsePersistence<InMemoryPersistence>();
            cfg.Conventions()
                .DefiningCommandsAs( t => t.Namespace != null && t.Namespace.EndsWith( ".Commands" ) )
                .DefiningEventsAs( t => t.Namespace != null && t.Namespace.EndsWith( ".Events" ) );

            var endpoint = await Endpoint.Start(cfg).ConfigureAwait(false);

            await endpoint.Subscribe<Messages.Events.IHaveDoneSomething>().ConfigureAwait(false);

            Console.Read();

            await endpoint.Stop().ConfigureAwait(false);
        }
    }
}