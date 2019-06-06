using System;
using System.Collections.Generic;
using AltV.Net.NetworkingEntity;
using AltV.Net.NetworkingEntity.Elements.Factories;
using AltV.Net.NetworkingEntity.Elements.Providers;
using Entity;

namespace AltV.Net.Networking.Example
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            AltNetworking.Init(new NetworkingModule(new IdProvider(), new NetworkingEntityFactory(),
                new ClientTokenProvider(), new NetworkingClientFactory(),
                new NonePlayerAuthenticationProviderFactory(), new ClientEntityStreamingHandlerFactory()));
            var data = new Dictionary<string, object>();
            var data2 = new Dictionary<string, object>();
            data["bla"] = "123";
            data["bla2"] = 1235;
            AltNetworking.CreateEntity(new Position {X = 0, Y = 0, Z = 0}, 1, 50, data);
            AltNetworking.CreateEntity(new Position {X = 1, Y = 1, Z = 1}, 1, 50, data2);
            AltNetworking.OnEntityStreamIn = (entity, client) => { Console.WriteLine("streamed in " + entity.Id + " in client " + client.Token); };
            AltNetworking.OnEntityStreamOut = (entity, client) => { Console.WriteLine("streamed out " + entity.Id + " in client " + client.Token); };
            Console.ReadKey();
        }
    }
}