using HarmonyLib;
using LordAshes;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace CameraToolsPlugin.Patches
{

    [HarmonyPatch(typeof(CutVolume), "Awake")]
    internal class CutVolumeAwakePatch
    {
        internal static void Postfix(ref Material _cutCapMaterial)
        {
            Debug.Log("CCP:Patching"); 
            var bundle = FileAccessPlugin.AssetBundle.Load($"CustomData/SkyMap/{CameraToolsPlugin.bundle.Value}");
            var mat = bundle.LoadAsset<Material>(CameraToolsPlugin.skyBox.Value);
            _cutCapMaterial = mat;
            Debug.Log(mat == null);
            Debug.Log("CCP:Patch Complete");
        }

        AtmosphereManager
    }

    [HarmonyPatch(typeof(CameraController), "Awake")]
    internal class CameraControllerAwakePatch
    {
        internal static void Postfix(ref Camera ____camera)
        {
            Debug.Log("CCP:Patching");
            ____camera.clearFlags = CameraClearFlags.Skybox;
            var bundle = FileAccessPlugin.AssetBundle.Load($"CustomData/SkyMap/{CameraToolsPlugin.bundle.Value}");
            var mat = bundle.LoadAsset<Material>(CameraToolsPlugin.skyBox.Value);
            RenderSettings.skybox = mat;
            Debug.Log(mat == null);
            Debug.Log("CCP:Patch Complete");
        }
    }

    [HarmonyPatch(typeof(RootTargetCameraMode), "Awake")]
    internal class RootTargetCameraModeAwakePatch
    {
        internal static void Postfix(ref float ___maxTilt, ref float ___minTilt)
        {
            ___minTilt = CameraToolsPlugin.minTilt.Value;
            ___maxTilt = CameraToolsPlugin.maxTilt.Value;

            
        }
    }
}
