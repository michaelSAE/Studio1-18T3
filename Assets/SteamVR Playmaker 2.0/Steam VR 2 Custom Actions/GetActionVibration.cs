using UnityEngine;
using Valve.VR;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("SteamVR 2.0")]
    [Tooltip("Gets all information from the action pose, including velocity, position and rotation.")]
    public class GetActionVibration : FsmStateAction
    {
        public SteamVR_Input_Sources device;
        public SteamVR_Action_Vibration hapticAction;

        //public FsmFloat secondsFromNow = 0;
        public FsmFloat duration = 0;

        [HasFloatSlider(0, 320)]
        public FsmFloat frequency = .5f;
        [HasFloatSlider(0, 1)]
        public FsmFloat amplitude = 1;

        [Tooltip("Event to send once duration is complete.")]
        public FsmEvent sendEvent;

        public override void Reset()
        {
            //secondsFromNow = 1;
            duration = .1f;
            frequency = 100f;
            amplitude = .1f;
            sendEvent = null;
        }

        public override void OnEnter()
        {
            if (hapticAction == null)
            {
                Debug.LogError("Missing haptic Action : " + Owner.name);
                Finish();
            }

        StartCoroutine(DoStartCoroutine());


        }


        IEnumerator DoStartCoroutine()
        {

            float startTime = FsmTime.RealtimeSinceStartup;
            while (FsmTime.RealtimeSinceStartup - startTime <= duration.Value)
            {
                hapticAction.Execute(0, duration.Value, frequency.Value, amplitude.Value, device);
                yield return null;
            }

                Fsm.Event(sendEvent);
        }
    }

}