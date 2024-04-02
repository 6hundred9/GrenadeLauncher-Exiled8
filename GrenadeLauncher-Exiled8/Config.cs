using System.ComponentModel;
using Exiled.API.Interfaces;
using GrenadeLauncher_Exiled8.Items;

namespace GrenadeLauncher_Exiled8
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        
        public GrenadeLauncher grenadeLauncher { get; set; } = new();
    }
}