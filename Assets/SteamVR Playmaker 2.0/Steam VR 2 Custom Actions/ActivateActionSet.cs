/*
Select event for system selection.
*/
using UnityEngine;
using Valve.VR;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("SteamVR 2.0")]
    [Tooltip("Activates all actions in an action set.")]
    public class ActivateActionSet : FsmStateAction
    {
        public SteamVR_ActionSet actionSet;
        public FsmBool activate = true;

        public override void Reset()
        {
            activate = true;
        }

        public override void OnEnter()
        {
            if (actionSet == null)
            {
                Debug.LogError("Missing Action Set : " + Owner.name);
                Finish();
            }

            if (activate.Value) 
                actionSet.ActivatePrimary();
            else
            {
                actionSet.Deactivate();
            }
        }

        
    }
}