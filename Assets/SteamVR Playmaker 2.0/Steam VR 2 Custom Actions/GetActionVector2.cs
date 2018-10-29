using UnityEngine;
using System;
using Valve.VR;


namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("SteamVR 2.0")]
    [Tooltip("Useful for getting the information on analog controllers or touchpads.")]
    public class GetActionVector2 : FsmStateAction
    {
   
        public SteamVR_Input_Sources device;

        public SteamVR_Action_Vector2 vector2Action;

        public setTriggerType vector2Type;

        public enum setTriggerType
        {
            getAxis,
            getAxisDelta,
            getLastAxis,
            getLastAxisDelta,
        }

        [RequiredField]
        [UIHint(UIHint.Variable)]
        [Tooltip("Store the result in a float variable.")]
        public FsmVector2 storeVector2Result;

        private Vector2 result;

        public override void OnEnter()
        {
            if (vector2Action == null)
            {
                Debug.LogError ("Missing Vector 2 Action : " + Owner.name);
                Finish();
            }
        }

        public override void Reset()
        {
            storeVector2Result = null;
        }

        public override void OnUpdate()
        {
            switch (vector2Type)
            {
                case setTriggerType.getAxis:
                    result = vector2Action.GetAxis(device);
                    break;
                case setTriggerType.getAxisDelta:
                    result = vector2Action.GetAxisDelta(device);
                    break;
                case setTriggerType.getLastAxis:
                    result = vector2Action.GetLastAxis(device);
                    break;
                case setTriggerType.getLastAxisDelta:
                    result = vector2Action.GetLastAxisDelta(device);
                    break;
            }

            storeVector2Result.Value = result;
        }
    }
}
