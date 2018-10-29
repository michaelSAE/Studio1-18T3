using UnityEngine;
using System;
using Valve.VR;


namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("SteamVR 2.0")]
    [Tooltip("Get any Singles from the controller set in binding. Useful for getting the triggers on controllers.")]
    public class GetActionSingle : FsmStateAction
    {
   
        public SteamVR_Input_Sources device;

        public SteamVR_Action_Single singleAction;

        public setTriggerType singleType;

        public enum setTriggerType
        {
            getAxis,
            getAxisDelta,
            getLastAxis,
            getLastAxisDelta,
        }

        [Tooltip("Axis values are in the range -1 to 1. Use the multiplier to set a larger range.")]
        public FsmFloat multiplier = 1;

        private float result;

        [RequiredField]
        [UIHint(UIHint.Variable)]
        [Tooltip("Store the result in a float variable.")]
        [Title("Store Float Result")]
        public FsmFloat store;

        public override void Reset()
        {
            multiplier = 1;
            store = null;
        }

        public override void OnEnter()
        {
            if (singleAction == null)
            {
                Debug.LogError("Missing Single Action : " + Owner.name);
                Finish();
            }
        }

        public override void OnUpdate()
        {
            switch (singleType)
            {
                case setTriggerType.getAxis:
                    result = singleAction.GetAxis(device);
                    break;
                case setTriggerType.getAxisDelta:
                    result = singleAction.GetAxisDelta(device);
                    break;
                case setTriggerType.getLastAxis:
                    result = singleAction.GetLastAxis(device);
                    break;
                case setTriggerType.getLastAxisDelta:
                    result = singleAction.GetLastAxisDelta(device);
                    break;
            }

            // if variable set to none, assume multiplier is 1
            if (!multiplier.IsNone)
            {
                result *= multiplier.Value;
            }

            store.Value = result;


        }
    }
}
