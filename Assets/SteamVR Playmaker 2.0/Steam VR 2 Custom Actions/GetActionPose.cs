using UnityEngine;
using Valve.VR;


namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("SteamVR 2.0")]
    [Tooltip("Gets all information from the action pose, including velocity, position and rotation.")]
    public class GetActionPose : FsmStateAction
    {
        public SteamVR_Input_Sources device;

        public SteamVR_Action_Pose poseAction;

        [ActionSection("Get Velocity")]
        [Tooltip("Select Velocity type.")]
        public setVelocityType velocityType;

        [Tooltip("Get the Vector3 Velocity on any device.")]
        [UIHint(UIHint.Variable)]
        [Title("Store Vector3 Velocity")]
        public FsmVector3 storeVelocity;

        [ActionSection("Get Angular Velocity")]
        [Tooltip("Select Velocity type.")]
        public setaVelocityType angularVelocityType;

        [Tooltip("Get the Vector3 Angular Velocity on any device.")]
        [UIHint(UIHint.Variable)]
        [Title("Store Vector3 Angular")]
        public FsmVector3 storeAngularVelocity;

        private Vector3 velocity;

        public enum setaVelocityType
        {
            getAngularVelocity,
            getLastStateAngularVelocity,
        };


        public enum setVelocityType
        {
            getVelocity,
            getLastStateVelocity,
        };

        [ActionSection("Get Position")]
        [Tooltip("Select the type of position, either local or last local.")]
        public setPositionType positionType;

        public enum setPositionType
        {
            getLocalPosition,
            getLastLocalPosition,
        };

        [Tooltip("Set to True if the button is pressed.")]
        [UIHint(UIHint.Variable)]
        [Title("Store Vector3 Position")]
        public FsmVector3 storePosition;

        [ActionSection("Get Rotation")]
        [Tooltip("Select current state or last state between trigger types.")]
        public setRotationType rotationType;



        public enum setRotationType
        {
            getLocalRotation,
            getLastLocalRotation,
        };

        [Tooltip("Set to True if the button is pressed.")]
        [UIHint(UIHint.Variable)]
        [Title("Store Quaternion Rotation")]
        public FsmQuaternion storeRotation;


        public override void Reset()
        {
            storeVelocity = null;
            storeAngularVelocity = null;
            storePosition = null;
            storeRotation = null;
        }

        public override void OnEnter()
        {
            if (poseAction == null)
            {
                Debug.LogError("Missing Pose Action : " + Owner.name);
                Finish();
            }
        }

        public override void OnUpdate()
        {
            DoGetVelocity();
            DoGetAngularVelocity();

            switch (positionType)
            {
                case setPositionType.getLocalPosition:
                    storePosition.Value = poseAction.GetLocalPosition(device);
                    break;
                case setPositionType.getLastLocalPosition:
                    storePosition.Value = poseAction.GetLastLocalPosition(device);
                    break;

            }
            switch (rotationType)
            {

                case setRotationType.getLocalRotation:
                    storeRotation.Value = poseAction.GetLocalRotation(device);
                    break;
                case setRotationType.getLastLocalRotation:
                    storeRotation.Value = poseAction.GetLastLocalRotation(device);
                    break;
            }
        }
        void DoGetVelocity()
        {

            switch (velocityType)
            {
                case setVelocityType.getVelocity:
                    velocity = poseAction.GetVelocity(device);
                    break;
                case setVelocityType.getLastStateVelocity:
                    velocity = poseAction.GetLastVelocity(device);
                    break;
            }

            storeVelocity.Value = velocity;
        }
        void DoGetAngularVelocity()
        {

            switch (angularVelocityType)
            {
                case setaVelocityType.getAngularVelocity:
                    velocity = poseAction.GetVelocity(device);
                    break;
                case setaVelocityType.getLastStateAngularVelocity:
                    velocity = poseAction.GetLastVelocity(device);
                    break;
            }

            storeAngularVelocity = velocity;
        }

    }
}