using UnityEngine;
using Valve.VR;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("SteamVR 2.0")]
    [Tooltip("Gets the current state of the boolean. Useful for any type of button.")]
    public class GetActionBoolean : FsmStateAction
    {
        public SteamVR_Input_Sources device;
        public SteamVR_Action_Boolean booleanAction;

        [Tooltip("Select current state or last state between boolean types.")]
        public setTriggerType booleanType;

        public enum setTriggerType
        {
            getState,
            getStateDown,
            getStateUp,
            getLastState,
            getLastStateDown,
            getLastStateUp,
        };

        [Tooltip("Event to send if the button is pressed.")]
        public FsmEvent sendEvent;

        [Tooltip("Set to True if the button is pressed.")]
        [UIHint(UIHint.Variable)]
        [Title("Store Bool Result")]
        public FsmBool storeResult;

        public override void Reset()
        {
            sendEvent = null;
            storeResult = null;
        }

        public override void OnEnter()
        {
            if (booleanAction == null)
            {
                Debug.LogError("Missing Boolean Action : " + Owner.name);
                Finish();
            }
        }

        public override void OnUpdate()
        {
            switch (booleanType)
            {
                case setTriggerType.getState:
                    var buttonDown = booleanAction.GetState(device);

                    if (buttonDown)
                    {
                        Fsm.Event(sendEvent);
                    }
                    storeResult.Value = buttonDown;
                    break;
                case setTriggerType.getStateUp:
                    var buttonDownUp = booleanAction.GetStateUp(device);

                    if (buttonDownUp)
                    {
                        Fsm.Event(sendEvent);
                    }
                    storeResult.Value = buttonDownUp;
                    break;
                case setTriggerType.getStateDown:
                    var buttonDownDown = booleanAction.GetStateDown(device);

                    if (buttonDownDown)
                    {
                        Fsm.Event(sendEvent);
                    }
                    storeResult.Value = buttonDownDown;
                    break;
                case setTriggerType.getLastState:
                    var buttonTouch = booleanAction.GetLastState(device);

                    if (buttonTouch)
                    {
                        Fsm.Event(sendEvent);
                    }
                    storeResult.Value = buttonTouch;
                    break;
                case setTriggerType.getLastStateUp:
                    var buttonTouchUp = booleanAction.GetLastStateUp(device);

                    if (buttonTouchUp)
                    {
                        Fsm.Event(sendEvent);
                    }
                    storeResult.Value = buttonTouchUp;
                    break;
                case setTriggerType.getLastStateDown:
                    var buttonTouchDown = booleanAction.GetLastStateDown(device);

                    if (buttonTouchDown)
                    {
                        Fsm.Event(sendEvent);
                    }
                    storeResult.Value = buttonTouchDown;
                    break;
            }
        }

    }
}