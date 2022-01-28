using UnityEngine;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;

namespace CameraToolsPlugin
{

    [BepInPlugin(Guid, "HolloFoxes' Camera Tools Plug-In", Version)]
    public class CameraToolsPlugin : BaseUnityPlugin
    {
        // constants
        public const string Guid = "org.hollofox.plugins.CameraToolsPlugin";
        internal const string Version = "3.0.0";

        // Configs
        internal static ConfigEntry<float> minTilt { get; set; }
        internal static ConfigEntry<float> maxTilt { get; set; }
        /// <summary>
        /// Awake plugin
        /// </summary>
        void Awake()
        {
            Logger.LogInfo("In Awake for Camera Tools");

            minTilt = Config.Bind("Tilt Limit", "minimum", -124f);
            maxTilt = Config.Bind("Tilt Limit", "maximum", 53f);

            Debug.Log("CameraTools Plug-in loaded");

            ModdingTales.ModdingUtils.Initialize(this, Logger);
            var harmony = new Harmony(Guid);
            harmony.PatchAll();
        }
    }
}
