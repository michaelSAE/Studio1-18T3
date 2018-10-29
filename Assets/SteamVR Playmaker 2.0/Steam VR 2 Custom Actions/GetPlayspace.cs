using UnityEngine;
using System.Collections;
using Valve.VR;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("SteamVR 2.0")]
    [Tooltip("Gets the area of play from the player.")]
    public class GetPlaySpace : FsmStateAction
    {


        public enum Size
        {
            Calibrated,
            _400x300,
            _300x225,
            _200x150
        }

        private Vector3[] vertices;

        public Size size;

        public FsmVector3 vector3Size;

        public override void OnEnter()
        {
            resize();
        }

        public static bool GetBounds(Size size, ref HmdQuad_t pRect)
        {
            if (size == Size.Calibrated)
            {
                var initOpenVR = (!SteamVR.active && !SteamVR.usingNativeSupport);
                if (initOpenVR)
                {
                    var error = EVRInitError.None;
                    OpenVR.Init(ref error, EVRApplicationType.VRApplication_Other);
                }

                var chaperone = OpenVR.Chaperone;
                bool success = (chaperone != null) && chaperone.GetPlayAreaRect(ref pRect);
                if (!success)
                    Debug.LogWarning("Failed to get Calibrated Play Area bounds!  Make sure you have tracking first, and that your space is calibrated.");

                if (initOpenVR)
                    OpenVR.Shutdown();

                return success;
            }
            else
            {
                try
                {
                    var str = size.ToString().Substring(1);
                    var arr = str.Split(new char[] { 'x' }, 2);

                    // convert to half size in meters (from cm)
                    var x = float.Parse(arr[0]) / 200;
                    var z = float.Parse(arr[1]) / 200;

                    pRect.vCorners0.v0 = x;
                    pRect.vCorners0.v1 = 0;
                    pRect.vCorners0.v2 = z;

                    pRect.vCorners1.v0 = x;
                    pRect.vCorners1.v1 = 0;
                    pRect.vCorners1.v2 = z;

                    pRect.vCorners2.v0 = x;
                    pRect.vCorners2.v1 = 0;
                    pRect.vCorners2.v2 = z;

                    pRect.vCorners3.v0 = x;
                    pRect.vCorners3.v1 = 0;
                    pRect.vCorners3.v2 = z;

                    return true;
                }
                catch { }
            }

            return false;
        }

        public void resize()
        {
            var rect = new HmdQuad_t();
            if (!GetBounds(size, ref rect))
                return;

            var corners = new HmdVector3_t[] { rect.vCorners0, rect.vCorners1, rect.vCorners2, rect.vCorners3 };

            vertices = new Vector3[corners.Length * 2];
            for (int i = 0; i < corners.Length; i++)
            {
                var c = corners[i];
                vertices[i] = new Vector3(c.v0, 0.01f, c.v2);
                vector3Size.Value = (new Vector3(c.v0, 0f, c.v2));
            }

        }
    }
}