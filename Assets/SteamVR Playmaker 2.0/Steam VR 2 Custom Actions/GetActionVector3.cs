using UnityEngine;
using System;
using Valve.VR;


namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("SteamVR 2.0")]
    [Tooltip("Gets any Vector3s from the controller set in binding.")]
    public class GetActionVector3 : FsmStateAction
    {
   
        public SteamVR_Input_Sources device;

        public SteamVR_Action_Vector3 vector3Action;

        public setTriggerType vector3Type;


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
        [Title("Store Vector3 Result")]
        public FsmVector3 storeResult;

        private Vector3 result;

        public override void Reset()
        {
            storeResult = null;
        }

        public override void OnEnter()
        {
            if (vector3Action == null)
            {
                Debug.LogError("Missing Vector 3 Action : " + Owner.name);
                Finish();
            }
        }

        public override void OnUpdate()
        {
            switch (vector3Type)
            {
                case setTriggerType.getAxis:
                    storeResult = vector3Action.GetAxis(device);
                    break;
                case setTriggerType.getAxisDelta:
                    storeResult = vector3Action.GetAxisDelta(device);
                    break;
                case setTriggerType.getLastAxis:
                    storeResult = vector3Action.GetLastAxis(device);
                    break;
                case setTriggerType.getLastAxisDelta:
                    storeResult = vector3Action.GetLastAxisDelta(device);
                    break;
            }
            storeResult.Value = result;
        }
    }
}
