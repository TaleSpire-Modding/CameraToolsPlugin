using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using PluginUtilities;
using BepInEx.Logging;
using CameraToolsPlugin.Patches;

namespace CameraToolsPlugin
{

    [BepInPlugin(Guid, "Camera Tools Plug-In", Version)]
    [BepInDependency(SetInjectionFlag.Guid)]
    public partial class CameraToolsPlugin : DependencyUnityPlugin<CameraToolsPlugin>
    {
        // constants
        public const string Guid = "org.hollofox.plugins.CameraToolsPlugin";
        internal const string Version = "0.0.0.0";
        internal static ManualLogSource logSource;

        // Configs
        internal static ConfigEntry<float> MinTilt { get; set; }
        internal static ConfigEntry<float> MaxTilt { get; set; }

        Harmony harmony;

        
        protected override void OnSetupConfig(ConfigFile config)
        {
            ConfigDescription minTiltDescription = new ConfigDescription("", null,
                new ConfigurationAttributes
                {
                    CallbackAction = UpdateTiltFromConfig
                });

            ConfigDescription maxTiltDescription = new ConfigDescription("", null,
                new ConfigurationAttributes
                {
                    CallbackAction = UpdateTiltFromConfig
                });

            MinTilt = Config.Bind("Tilt Limit", "minimum", -124f, minTiltDescription);
            MaxTilt = Config.Bind("Tilt Limit", "maximum", 53f, maxTiltDescription);
        }

        /// <summary>
        /// Awake plugin
        /// </summary>
        protected override void OnAwake()
        {
            logSource = Logger;
            Logger.LogInfo("In Awake for Camera Tools");

            harmony = new Harmony(Guid);
            harmony.PatchAll();

            // Apply initial tilt values if the camera mode is already awake
            RootTargetCameraModeAwakePatch.instance = FindAnyObjectByType<RootTargetCameraMode>();
            if (RootTargetCameraModeAwakePatch.instance != null)
            {
                RootTargetCameraModeAwakePatch.UpdateTilt(MinTilt.Value, MaxTilt.Value);
            }
        }

        /// <summary>
        /// Discarded parameter, just needed to know it was updated
        /// </summary>
        private void UpdateTiltFromConfig(object o)
        {
            if (Enabled)
                RootTargetCameraModeAwakePatch.UpdateTilt(MinTilt.Value, MaxTilt.Value);
        }

        protected override void OnDestroyed()
        {
            // Reset to default values
            if (RootTargetCameraModeAwakePatch.instance != null)
            {
                RootTargetCameraModeAwakePatch.UpdateTilt(-18f, 42f);
            }

            // Clear instance
            RootTargetCameraModeAwakePatch.instance = null;

            // Unpatch Harmony patches
            harmony.UnpatchSelf();
        }
    }
}
