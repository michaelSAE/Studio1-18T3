using UnityEngine;
using Exploder.Utils;

namespace Exploder.Demo
{
    public class QuickStartDemo : MonoBehaviour
    {
        private GameObject sphereObject;

        private void Start()
        {
            sphereObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphereObject.transform.position = Vector3.zero;
        }

        private void OnGUI()
        {
            if (sphereObject.activeSelf)
            {
                if (GUI.Button(new Rect(10, 10, 120, 70), "Explode sphere"))
                {
                    ExploderSingleton.Instance.ExplodeObject(sphereObject);
                }
            }
            else
            {
                if (GUI.Button(new Rect(10, 10, 120, 70), "Create sphere"))
                {
                    sphereObject.SetActive(true);
                }
            }
        }
    }
}
