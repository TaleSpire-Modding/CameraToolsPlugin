using System.Linq;
using UnityEngine;
using BepInEx;
using BepInEx.Configuration;
using CameraToolsPlugin.Patches;
using HarmonyLib;


namespace CameraToolsPlugin
{

    [BepInPlugin(Guid, "HolloFoxes' Camera Tools Plug-In", Version)]
    public partial class CameraToolsPlugin : BaseUnityPlugin
    {
        // constants
        public const string Guid = "org.hollofox.plugins.CameraToolsPlugin";
        internal const string Version = "3.1.2";

        

        // Configs
        internal static ConfigEntry<float> minTilt { get; set; }
        internal static ConfigEntry<float> maxTilt { get; set; }

        // Configs
        internal static ConfigEntry<KeyCode> setLocale { get; set; }
        internal static ConfigEntry<KeyCode> render { get; set; }

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

            setLocale = Config.Bind("Ortho Render", "setLocale", KeyCode.P);
            render = Config.Bind("Ortho Render", "render", KeyCode.U);

            skyBox = Config.Bind("Sky Box", "box name", "DarkStorm");
            bundle = Config.Bind("Sky Box", "bundle name", "hfskyboxes01");


            Debug.Log("CameraTools Plug-in loaded");

            ModdingTales.ModdingUtils.Initialize(this, Logger);
            var harmony = new Harmony(Guid);
            harmony.PatchAll();
        }

        void Update()
        { 
            
            if (Input.GetKeyUp(setLocale.Value))
            {
                SystemMessage.AskForTextInput("Size of Render", "Rectangle area to render in Tiles","sure",myAction, delegate { });
            }

            if (Input.GetKeyUp(render.Value))
            {
                if(!OrtoCameraModeAwakePatch.isOverRideHeight) OrtoCameraModeAwakePatch.SetResolution();
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
