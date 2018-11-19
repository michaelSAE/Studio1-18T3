using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Valve.VR;
using Valve.VR.InteractionSystem;


public class CueHandler : MonoBehaviour {

    public Hand frontHand;
    public Hand backHand;

    private Vector3 frontPos;
    private Vector3 backPos;

    private Rigidbody cueRB;
    private Vector3 cuePos;

    // allows rear hand to slide cue forward and back.
    private float lockOffset;

    // locks forward hand pos. 
    private Vector3 lockForward;

    // position of the cue tip
    public Transform cueTip;



    // Use this for initialization
    void Awake()
    {

        cueRB = gameObject.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        updateCuePosition();
       
    }

    void updateCuePosition()
    {
       Vector3  frontPos = frontHand.transform.position;
       Vector3 backPos = backHand.transform.position;

        cueRB.MovePosition(0.75f * backPos + 0.25f * frontPos);
        cueRB.MoveRotation(Quaternion.LookRotation(frontPos - backPos) * Quaternion.Euler(90f, 0f, 0f));

    }

    // LOCK THE Cue's rotation. 
    public void freezeCue()
    {
        lockForward = transform.up;
        lockOffset = (frontPos - backPos).magnitude;
        Debug.Log("CUE IS in A LOCKED POSITION");
    }

    public void heldCue()
    {
        float currOffset = (frontPos - backPos).magnitude;
        cueRB.MovePosition(cuePos + lockForward * (lockOffset - currOffset));
        Debug.Log("CUE IS HELD in  POSITION");
    }


}
