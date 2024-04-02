using Exiled.API.Features;
using Exiled.CustomItems.API;

namespace GrenadeLauncher_Exiled8
{
    public class Plugin : Plugin<Config>
    {
        public static Plugin Instance;
        public override void OnEnabled()
        {
            Instance = this;
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