using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Exploder.Utils;

public class explodeOnImpact : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Explode")
        {
            ExploderSingleton.Instance.ExplodeObject(gameObject);
        }

    }

}



