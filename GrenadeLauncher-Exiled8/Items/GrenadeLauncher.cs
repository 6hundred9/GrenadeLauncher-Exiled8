using System.Collections.Generic;
using CustomPlayerEffects;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Items;
using Exiled.API.Features.Roles;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;

namespace GrenadeLauncher_Exiled8.Items
{
    [CustomItem(ItemType.GunRevolver)]
    public class GrenadeLauncher : CustomWeapon
    {
        public override uint Id { get; set; } = 1;
        public override string Name { get; set; } = "Grenade Launcher";
        public override float Damage { get; set; } = 0f;
        public override string Description { get; set; } = "Shoot your opponents, opponent go kaboom, as shrimple as that";
        public override float Weight { get; set; } = 0.5f;

        public override SpawnProperties SpawnProperties { get; set; } = new()
        {
            Limit = 50,
            DynamicSpawnPoints = new List<DynamicSpawnPoint>
            {
                new DynamicSpawnPoint { Chance = 75, Location = SpawnLocationType.Inside079Secondary },
                new DynamicSpawnPoint { Chance = 30, Location = SpawnLocationType.InsideHidLeft }
            }
        };

        protected override void OnShooting(ShootingEventArgs ev)
        {
            if (!Check(ev.Firearm)) return;
            if (ev.Player.CurrentItem is Firearm firearm)
                firearm.MaxAmmo = 1;
            ExplosiveGrenade grenade = (ExplosiveGrenade)Item.Create(ItemType.GrenadeHE);
            grenade.FuseTime = 0.1f;
            grenade.SpawnActive(ev.ShotPosition, ev.Player);
        }

    }
}