using UnityEngine;
using System.Collections;

public class explosionController : MonoBehaviour
{
    private ParticleSystem particle;
	
	void Start () { particle = gameObject.GetComponent<ParticleSystem>(); }
	void Update () { if(!particle.isPlaying) Destroy(gameObject); }

    public void setPosition(Vector3 pos) { transform.position = pos; }
}
