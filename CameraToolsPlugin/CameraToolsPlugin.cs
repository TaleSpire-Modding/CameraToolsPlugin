using System.Linq;
using UnityEngine;
using BepInEx;
using BepInEx.Configuration;
using CameraToolsPlugin.Patches;
using HarmonyLib;
using PluginUtilities;


namespace CameraToolsPlugin
{

    [BepInPlugin(Guid, "Camera Tools Plug-In", Version)]
    [BepInDependency(SetInjectionFlag.Guid)]
    public partial class CameraToolsPlugin : BaseUnityPlugin
    {
        // constants
        public const string Guid = "org.hollofox.plugins.CameraToolsPlugin";
        internal const string Version = "0.0.0.0";

        // Configs
        internal static ConfigEntry<float> minTilt { get; set; }
        internal static ConfigEntry<float> maxTilt { get; set; }

        // Configs
        internal static ConfigEntry<KeyboardShortcut> setLocale { get; set; }
        internal static ConfigEntry<KeyboardShortcut> render { get; set; }
        internal static ConfigEntry<int> pxTile { get; set; }
        internal static ConfigEntry<bool> othroRenderEnabled { get; set; }

        // Configs
        // internal static ConfigEntry<string> skyBox { get; set; }
        // internal static ConfigEntry<string> bundle { get; set; }

        /// <summary>
        /// Awake plugin
        /// </summary>
        void Awake()
        {
            Logger.LogInfo("In Awake for Camera Tools");

            minTilt = Config.Bind("Tilt Limit", "minimum", -124f);
            maxTilt = Config.Bind("Tilt Limit", "maximum", 53f);

            othroRenderEnabled = Config.Bind("Ortho Render", "Enabled", false, "Ortho feature is enabled for use");
            setLocale = Config.Bind("Ortho Render", "setLocale shortcut", new KeyboardShortcut(KeyCode.P), "Key to save coordinates used to determine render area.");
            render = Config.Bind("Ortho Render", "render shortcut", new KeyboardShortcut(KeyCode.U), "Key to get the camera to render the screen shot");
            pxTile = Config.Bind("Ortho Render", "Pixels per Tile", 75,"Pixels per tile is an approximation thus an average");
            
            // skyBox = Config.Bind("Sky Box", "box name", "DarkStorm");
            // bundle = Config.Bind("Sky Box", "bundle name", "hfskyboxes01");

            Debug.Log("CameraTools Plug-in loaded");

            ModdingTales.ModdingUtils.Initialize(this, Logger, "HolloFoxes'");
            var harmony = new Harmony(Guid);
            harmony.PatchAll();
        }

        void Update()
        {
            if (!othroRenderEnabled.Value) 
                return;

            if (setLocale.Value.IsUp())
            {
                SystemMessage.AskForTextInput("Size of Render", "Rectangle area to render in Tiles", "sure",
                    myAction, delegate { });
            }

            if (render.Value.IsUp())
            {
                if (!OrtoCameraModeAwakePatch.isOverRideHeight) OrtoCameraModeAwakePatch.SetResolution();
                OrtoCameraModeAwakePatch.isOverRideHeight = !OrtoCameraModeAwakePatch.isOverRideHeight;
            }
            
            if (OrtoCameraModeAwakePatch.isOverRideHeight)
            {
                CameraController.GetCamera().transform.position
                    .Set(OrtoCameraModeAwakePatch.xpos, OrtoCameraModeAwakePatch.camHeight, OrtoCameraModeAwakePatch.zpos);
            }

            if (OrtoCameraModeAwakePatch.savePic)
            {
                var cam = CameraController.GetCamera();
                OrtoCameraModeAwakePatch.RTImage(cam);
                OrtoCameraModeAwakePatch.savePic = false;
                Screen.SetResolution(OrtoCameraModeAwakePatch.res.width, OrtoCameraModeAwakePatch.res.height, true);
                OrtoCameraModeAwakePatch.isOverRideHeight = false;
            }
        }

        internal static Vector2 point1;
        internal static Vector2 point2;

        internal static void myAction(string size)
        {
            var x = size.Replace("(","").Replace(")","").Split(',')
                .Select(n => int.Parse(n.Replace(",",""))).ToArray();
            point1 = new Vector2(x[0],x[1]);
            point2 = new Vector2(x[2],x[3]);
        }
    }
}
