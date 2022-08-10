using System;
using System.Collections;
using System.IO;
using HarmonyLib;
using Tweening;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using Screen = UnityEngine.Screen;

namespace CameraToolsPlugin.Patches
{
    [HarmonyPatch(typeof(ActiveCameraManager), "LateUpdate")]
    internal class ActiveCameraManagerPatch
    {
        internal static Camera _camera;
        internal static BoardSessionManager _boardSessionManager;
        internal static void Prefix(ref Camera ____camera, ref BoardSessionManager ____boardSessionManager)
        {
            _camera = ____camera;
            _boardSessionManager = ____boardSessionManager;
        }
    }

    [HarmonyPatch(typeof(OrtoCameraMode), "CallLateUpdate")]
    internal class OrtoCameraModeAwakePatch
    {
        internal static float invTan1 = 28.644980815379712343639073768556f;
        internal static bool isOverRideHeight;
        internal static float camHeight;
        internal static bool savePic;
        internal static Resolution res;

        internal static float xpos;
        internal static float zpos;

        internal static bool Prefix(
            ref Transform ___rotatorTransform, 
            ref Transform ___rootTransform, 
            ref Transform ___tiltTransform,
            ref float ___moveSpeedMultiplier,
            ref float ____height,
            ref CameraController ___controller
            )
        {
            if (isOverRideHeight)
            {
                Vector2 cameraMove = ControllerManager.CameraMove;
                Vector3 vector3_1 = ___rotatorTransform.right * cameraMove.x;
                Vector3 vector3_2 = ___rotatorTransform.forward * cameraMove.y * (Time.deltaTime * 8f);
                double num = (double)Time.deltaTime * 6.0;
                Vector3 vector3_3 = (vector3_1 * (float)num + vector3_2) * ___moveSpeedMultiplier;
                if (MouseManager.RightButton.IsDown())
                {
                    vector3_3.x -= MouseManager.groundDelta.x;
                    vector3_3.z -= MouseManager.groundDelta.z;
                }
                ___rootTransform.Translate(new Vector3(vector3_3.x, 0.0f, vector3_3.z), Space.World);
                ___tiltTransform.forward = Vector3.down;
                ____height = camHeight + 250f;
                ___rotatorTransform.transform.localPosition = (Vector3)new float3(0.0f, ____height, 0.0f);
                CameraController.FarNearPlane.Set(____height - 250f, ____height + 250f);
                ___controller.UpdateCameraPlaneHeightValue((float)(((double)____height - 251.0) * 0.0500000007450581));

                var cam = CameraController.GetCamera().transform;
                ___rootTransform.Translate(xpos - ___rootTransform.localPosition.x, 0,zpos - ___rootTransform.localPosition.z, Space.World);
                Debug.Log($"({Screen.currentResolution.width},{Screen.currentResolution.height})");
                res = Screen.currentResolution;
                Screen.SetResolution(75*width,75*height,true);
                Debug.Log($"({Screen.currentResolution.width},{Screen.currentResolution.height})");
                savePic = true;
                return false;
            }
            return true;
        }

        internal static int width;
        internal static int height;

        internal static void SetResolution()
        {
            var x1 = Convert.ToInt32(CameraToolsPlugin.point1.x);
            var x2 = Convert.ToInt32(CameraToolsPlugin.point2.x);

            var z1 = Convert.ToInt32(CameraToolsPlugin.point1.y);
            var z2 = Convert.ToInt32(CameraToolsPlugin.point2.y);

            width = Math.Abs(x1 - x2);
            height = Math.Abs(z1 - z2);

            xpos = Math.Min(x1, x2) + width / 2;
            zpos = Math.Min(z1, z2) + height/ 2;
            
            camHeight = CamHeight(height);
        }

        private static float CamHeight(int height)
        {
            return height * invTan1;
        }

        internal static float a;
        internal static Camera c;

        internal static void RTImage(Camera camera)
        {
            Camera Mcamera = camera;
            c = Mcamera;
            int rw = width * CameraToolsPlugin.pxTile.Value;
            int rh = height * CameraToolsPlugin.pxTile.Value;
            RenderTexture rt = new RenderTexture(rw, rh, 24);
            Mcamera.targetTexture = rt;
            Texture2D ss = new Texture2D(rw, rh, TextureFormat.RGB24, false);
            a = Mcamera.aspect;
            Mcamera.aspect = rw / rh;
            

            if (ActiveCameraManagerPatch._boardSessionManager == null)
                return;
            // using (NativeArray<Unity.Rendering.FrustumPlanes.PlanePacket4> planePacketsForCam = ZoneGpuState.GetPlanePacketsForCam(ActiveCameraManagerPatch._camera, ActiveCameraManagerPatch._tmpCamPlanes, Allocator.TempJob))
            //     ActiveCameraManagerPatch._boardSessionManager.OnRender(ActiveCameraManagerPatch._camera, planePacketsForCam);
            Mcamera.Render();

            RenderTexture.active = rt;
            ss.ReadPixels(new Rect(0, 0, rw, rh), 0, 0);
            
            Mcamera.targetTexture = null;
            RenderTexture.active = null;
            Mcamera.aspect = a;
            byte[] bytes = ss.EncodeToPNG();

            string filename = Path.Combine(FileB.EnsureDir(Path.Combine(Application.dataPath, "Photos")), "TS_" + DateTimeOffset.UtcNow.ToUnixTimeSeconds() + ".png");
            System.IO.File.WriteAllBytes(filename, bytes);
            Debug.Log(string.Format("Took screenshot to: {0}", filename));
        }
    }
}
