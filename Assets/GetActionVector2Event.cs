using UnityEngine;
using System;
using Valve.VR;


namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("SteamVR 2.0")]
    [Tooltip("Useful for getting the information on analog controllers or touchpads.")]
    public class GetActionVector2Event : FsmStateAction
    {
   
        public SteamVR_Input_Sources device;

        //public SteamVR_Action_Vector2 vector2Action;
        [ObjectType(typeof(SteamVR_Action_Vector2))]
        public FsmObject vector2Action;


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

        public FsmEvent sendEvent;

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
            var vector2 = vector2Action.Value as SteamVR_Action_Vector2;

            switch (vector2Type)
            {
                case setTriggerType.getAxis:
                    result = vector2.GetAxis(device);
                    break;
                case setTriggerType.getAxisDelta:
                    result = vector2.GetAxisDelta(device);
                    break;
                case setTriggerType.getLastAxis:
                    result = vector2.GetLastAxis(device);
                    break;
                case setTriggerType.getLastAxisDelta:
                    result = vector2.GetLastAxisDelta(device);
                    break;
            }

            storeVector2Result.Value = result;

            if (result.x > 0f || result.y > 0f) 
            Fsm.Event(sendEvent);

        }
    }
}
