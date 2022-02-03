using HarmonyLib;

namespace CameraToolsPlugin.Patches
{
    /*
    [HarmonyPatch(typeof(CameraController), "Awake")]
    internal class CameraControllerAwakePatch
    {
        internal static AssetBundle bundle;

        internal static void loadBundle()
        {
            if (bundle == null) bundle = FileAccessPlugin.AssetBundle.Load($"CustomData/SkyMap/{CameraToolsPlugin.bundle.Value}");
        }

        internal static void Postfix(ref Camera ____camera)
        {
            ____camera = new Camera();
            ____camera.targetTexture = null;

            Debug.Log("CCP:Patching");
            ____camera.clearFlags = CameraClearFlags.Skybox;
            loadBundle();
            var mat = bundle.LoadAsset<Material>(CameraToolsPlugin.skyBox.Value);
            RenderSettings.skybox = mat;

            Debug.Log("CCP:Patch Complete");
        }
    }*/

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
