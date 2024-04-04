using System.Collections.Generic;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Items;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using UnityEngine;
using MEC;
using PluginAPI.Core;
using Player = Exiled.API.Features.Player;
using Server = Exiled.API.Features.Server;

namespace GrenadeLauncher_Exiled8.Items
{
    [CustomItem(ItemType.GunRevolver)]
    public class GrenadeLauncher : CustomWeapon
    {
        public override uint Id { get; set; } = 1;
        public override string Name { get; set; } = "Grenade Launcher";
        public override float Damage { get; set; } = 0f;
        // ReSharper disable once StringLiteralTypo
        public override string Description { get; set; } = "Shoot your opponents, opponent go kaboom, as shrimple as that";
        public override float Weight { get; set; } = 0.5f;
        public override byte ClipSize { get; set; } = 8;
        public override ItemType Type { get; set; } = ItemType.GunRevolver;
        


        public override SpawnProperties SpawnProperties { get; set; } = new()
        {
            Limit = 50,
            DynamicSpawnPoints = new List<DynamicSpawnPoint>
            {
                new DynamicSpawnPoint { Chance = 75, Location = SpawnLocationType.Inside079Secondary },
                new DynamicSpawnPoint { Chance = 30, Location = SpawnLocationType.InsideHidLeft }
            }
        };
        protected override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Player.Shooting += OnShooting;
            Exiled.Events.Handlers.Player.PickingUpItem += OnPickingUp;
            Exiled.Events.Handlers.Player.ReloadingWeapon += OnReloading;
            base.SubscribeEvents();
        }
        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Player.Shooting -= OnShooting;
            Exiled.Events.Handlers.Player.PickingUpItem -= OnPickingUp;
            Exiled.Events.Handlers.Player.ReloadingWeapon -= OnReloading;
            base.UnsubscribeEvents();
        }

        protected override void OnPickingUp(PickingUpItemEventArgs ev)
        {
            if (ev.Player.HasItem(ItemType.GunRevolver) && ev.Pickup.Type == ItemType.GunRevolver)
            {
                ev.IsAllowed = false;
            }
            base.OnPickingUp(ev);
        }


        protected override void OnShooting(ShootingEventArgs ev)
        {
            if (!Check(ev.Firearm) && ev.Firearm.Type != ItemType.GunRevolver) return;
            ev.Player.ThrowGrenade(ProjectileType.FragGrenade);
            base.OnShooting(ev);
        }

        protected override void OnReloading(ReloadingWeaponEventArgs ev)
        {
            if (!Check(ev.Firearm) && ev.Firearm.Type != ItemType.GunRevolver) return;
            ev.IsAllowed = false;
            if (ev.Player.HasItem(ItemType.GrenadeHE) && ev.Firearm.Ammo != 1)
            {
                foreach (Item item in ev.Player.Items.Where(i => i.Type == ItemType.GrenadeHE))
                {
                    item.Destroy();
                    Timing.CallDelayed(5f, () => ev.Firearm.Ammo += 1);
                    break;
                }
            }
            base.OnReloading(ev);
        }
    }
}