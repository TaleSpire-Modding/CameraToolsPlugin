using HarmonyLib;
using Unity.Collections;
using UnityEngine;

namespace CameraToolsPlugin.Patches
{

    [HarmonyPatch(typeof(ActiveCameraManager), "LateUpdate")]
    internal class ACMPatch
    {
        internal static Plane[] _tmpCamPlanes;
        internal static void Prefix(ref Plane[] ____tmpCamPlanes)
        {
            _tmpCamPlanes = ____tmpCamPlanes;
        }
    }

    public class Scratch
    { 
        internal static void RenderImage(Camera camera)
        {
            if (BoardSessionManager.Instance == null) return;
            using (NativeArray<Unity.Rendering.FrustumPlanes.PlanePacket4> planePacketsForCam = ZoneGpuState.GetPlanePacketsForCam(camera, ACMPatch._tmpCamPlanes, Allocator.TempJob))
                BoardSessionManager.Instance.Render(camera, planePacketsForCam);
            camera.Render();
        }
    }
}