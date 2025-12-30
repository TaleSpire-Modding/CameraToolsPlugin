using HarmonyLib;
using System.Reflection;

namespace CameraToolsPlugin.Patches
{
    [HarmonyPatch(typeof(RootTargetCameraMode), "Awake")]
    internal class RootTargetCameraModeAwakePatch
    {
        internal static RootTargetCameraMode instance;
        
        internal static void Postfix(RootTargetCameraMode __instance, ref float ___maxTilt, ref float ___minTilt)
        {
            instance = __instance;
            ___minTilt = CameraToolsPlugin.MinTilt.Value;
            ___maxTilt = CameraToolsPlugin.MaxTilt.Value;
        }

        internal static void UpdateTilt(float min, float max)
        {
            if (instance == null)
                return;
            
            typeof(RootTargetCameraMode).GetField("minTilt", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(instance, min);
            typeof(RootTargetCameraMode).GetField("maxTilt", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(instance, max);
        }
    }
}
