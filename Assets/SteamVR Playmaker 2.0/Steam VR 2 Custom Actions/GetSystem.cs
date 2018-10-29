/*
Select event for system selection.
*/
using UnityEngine;
using Valve.VR;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("SteamVR 2.0")]
    [Tooltip("Sends an Event when a System button is Pressed.")]
    public class GetSystem : FsmStateAction
    {
        [Tooltip("Event to send if the system button is pressed.")]
        public FsmEvent sendEvent;

        [Tooltip("Check is system menu has been enabled.")]
        [UIHint(UIHint.Variable)]
        public FsmBool systemEnabled;
        SteamVR_Events.Action inputFocusAction;

        public override void Reset()
        {
            sendEvent = null;
            systemEnabled = null;
        }

        public override void Awake()
        {
            inputFocusAction = SteamVR_Events.InputFocusAction(OnInputFocus);

        }

        public override void OnEnter()
        {
            inputFocusAction.enabled = true;
        }

        private void OnInputFocus(bool hasFocus)
        {
            if (hasFocus)
            {
                Fsm.Event(sendEvent);
                systemEnabled.Value = !hasFocus;
            }
            else
            {
                Fsm.Event(sendEvent);
                systemEnabled.Value = !hasFocus;
            }
        }
    }
}