using System;
using Exiled.API.Features;
using Exiled.CustomItems.API;

namespace GrenadeLauncher_Exiled8
{
    public class Plugin : Plugin<Config>
    {
        public static Plugin Instance;
        public override string Author { get; } = "6hundred9";
        public override string Name { get; } = "Grenade Launcher";
        public override string Prefix { get; } = "grnLauncher";
        public override Version RequiredExiledVersion { get; } = new Version(8, 8, 1);

        public override void OnEnabled()
        {
            Instance = this;
            Config.grenadeLauncher.Type = ItemType.GunRevolver;
            Config.grenadeLauncher.Register();
            base.OnEnabled();
            
        }

        public override void OnDisabled()
        {
            Config.grenadeLauncher.Unregister();
            base.OnDisabled();
        }
        

    }
}