using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using PluginUtilities;
using BepInEx.Logging;

namespace CameraToolsPlugin
{

    [BepInPlugin(Guid, "Camera Tools Plug-In", Version)]
    [BepInDependency(SetInjectionFlag.Guid)]
    public partial class CameraToolsPlugin : BaseUnityPlugin
    {
        // constants
        public const string Guid = "org.hollofox.plugins.CameraToolsPlugin";
        internal const string Version = "0.0.0.0";
        internal static ManualLogSource logSource;

        // Configs
        internal static ConfigEntry<float> minTilt { get; set; }
        internal static ConfigEntry<float> maxTilt { get; set; }

        /// <summary>
        /// Awake plugin
        /// </summary>
        void Awake()
        {
            logSource = Logger;
            Logger.LogInfo("In Awake for Camera Tools");

            minTilt = Config.Bind("Tilt Limit", "minimum", -124f);
            maxTilt = Config.Bind("Tilt Limit", "maximum", 53f);

            Logger.LogDebug("CameraTools Plug-in loaded");

            ModdingTales.ModdingUtils.AddPluginToMenuList(this, "HolloFoxes'");
            var harmony = new Harmony(Guid);
            harmony.PatchAll();
        }
    }
}
