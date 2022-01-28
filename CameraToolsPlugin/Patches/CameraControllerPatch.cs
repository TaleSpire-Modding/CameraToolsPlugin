using HarmonyLib;
using Unity.Mathematics;
using UnityEngine;

namespace CameraToolsPlugin.Patches
{
    [HarmonyPatch(typeof(RootTargetCameraMode), "Awake")]
    internal class CameraControllerAwakePatch
    {
        internal static void Postfix(ref float ___maxTilt, ref float ___minTilt)
        {
            ___minTilt = CameraToolsPlugin.minTilt.Value;
            ___maxTilt = CameraToolsPlugin.maxTilt.Value;
        }
    }
}
