using UnityEngine;
using System.Collections;

public class LightSwitch : MonoBehaviour
{
    public GameObject light;
    public GameObject textO;
    public GameObject textC;

    void Start()
    {
        textC.SetActive(false);
        textO.SetActive(false);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            textO.SetActive(light.activeSelf);
            textC.SetActive(!light.activeSelf);
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            textO.SetActive(false);
            textC.SetActive(false);
        }
    }
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                // toggle the light. If off turn it on,  if on turn it off
                light.SetActive(!light.activeSelf);
                // update the texts based on the new active state.
                textO.SetActive(light.activeSelf);
                textC.SetActive(!light.activeSelf);
            }
        }
    }
}