/*
Slightly little difference between touch and press. Touch is registered faster than
press down. Press up is registered before touch up.
*/
using UnityEngine;
using Valve.VR;


namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("SteamVR 2.0")]
    [Tooltip("Fades the headset for a fixed duration.")]
    public class SteamHeadsetFade : FsmStateAction
    {
        [Tooltip("Set Fade Color.")]
        public FsmColor fadeColor;

        [Tooltip("Set duration of Fade.")]
        public FsmFloat duration;


        [Tooltip("Event to send if the button is pressed.")]
        public FsmEvent finishEvent;
        private float timer;


        public override void Reset()
        {
            fadeColor = Color.black;
            duration = 1f;
            finishEvent = null;
            
        }
        public override void OnEnter()
        {
            timer = 0f;
        }
        public override void OnUpdate()
        {
            SteamVR_Fade.Start(fadeColor.Value, duration.Value);


            timer += Time.deltaTime;


            if (timer >= duration.Value)
            {
                Finish();
                if (finishEvent != null)
                {
                    Fsm.Event(finishEvent);
                }
            }
        }
    }
}