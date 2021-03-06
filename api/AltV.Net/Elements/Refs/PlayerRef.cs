using System;
using AltV.Net.Elements.Entities;

namespace AltV.Net.Elements.Refs
{
    public readonly struct PlayerRef : IDisposable
    {
        private readonly IPlayer player;

        public bool Exists => player != null;

        public PlayerRef(IPlayer player)
        {
            this.player = player.AddRef() ? player : null;
            Alt.Module.CountUpRefForCurrentThread(player);
        }

        public void Dispose()
        {
            player?.RemoveRef();
            Alt.Module.CountDownRefForCurrentThread(player);
        }
    }
}