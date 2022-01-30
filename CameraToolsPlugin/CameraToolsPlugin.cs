using UnityEngine;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using LordAshes;

namespace CameraToolsPlugin
{

    [BepInPlugin(Guid, "HolloFoxes' Camera Tools Plug-In", Version)]
    [BepInDependency(FileAccessPlugin.Guid)]
    public class CameraToolsPlugin : BaseUnityPlugin
    {
        // constants
        public const string Guid = "org.hollofox.plugins.CameraToolsPlugin";
        internal const string Version = "3.0.0";

        // Configs
        internal static ConfigEntry<float> minTilt { get; set; }
        internal static ConfigEntry<float> maxTilt { get; set; }

        // Configs
        internal static ConfigEntry<string> skyBox { get; set; }
        internal static ConfigEntry<string> bundle { get; set; }

        /// <summary>
        /// Awake plugin
        /// </summary>
        void Awake()
        {
            Logger.LogInfo("In Awake for Camera Tools");

            minTilt = Config.Bind("Tilt Limit", "minimum", -124f);
            maxTilt = Config.Bind("Tilt Limit", "maximum", 53f);

            skyBox = Config.Bind("Sky Box", "box name", "DarkStorm");
            bundle = Config.Bind("Sky Box", "bundle name", "hfskyboxes01");


            Debug.Log("CameraTools Plug-in loaded");

            ModdingTales.ModdingUtils.Initialize(this, Logger);
            var harmony = new Harmony(Guid);
            harmony.PatchAll();
        }
    }
}
