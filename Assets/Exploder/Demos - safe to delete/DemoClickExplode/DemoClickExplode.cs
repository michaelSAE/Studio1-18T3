// to crack and explode use this macro
// crack by left mouse button, explode after by right mouse button
//#define ENABLE_CRACK_AND_EXPLODE
#define TEST_SCENE_LOAD

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Exploder.Demo
{
    public class DemoClickExplode : MonoBehaviour
    {
        private GameObject[] DestroyableObjects;
        private ExploderObject Exploder;
        public Camera Camera;

        private void Start()
        {
            Application.targetFrameRate = 60;

            //
            // access exploder from singleton
            //
            Exploder = Utils.ExploderSingleton.Instance;

            if (Exploder.DontUseTag)
            {
                var objs = FindObjectsOfType(typeof (Explodable));
                var objList = new List<GameObject>(objs.Length);
                objList.AddRange(from Explodable ex in objs where ex select ex.gameObject);
                DestroyableObjects = objList.ToArray();
            }
            else
            {
                // find all objects in the scene with tag "Exploder"
                DestroyableObjects = GameObject.FindGameObjectsWithTag("Exploder");
            }
        }

        private bool IsExplodable(GameObject obj)
        {
            if (Exploder.DontUseTag)
            {
                return obj.GetComponent<Explodable>() != null;
            }
            else
            {
                return obj.CompareTag(ExploderObject.Tag);
            }
        }

        private void Update()
        {
            // we hit the mouse button
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                Ray mouseRay;

                if (Camera)
                {
                    mouseRay = Camera.ScreenPointToRay(Input.mousePosition);
                }
                else
                {
                    mouseRay = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
                }

                RaycastHit hitInfo;

                // we hit the object
                if (Physics.Raycast(mouseRay, out hitInfo))
                {
                    var obj = hitInfo.collider.gameObject;

                    // explode this object!
                    if (IsExplodable(obj))
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            ExplodeObject(obj);
                        }
                        else
                        {
                            ExplodeAfterCrack(obj);
                        }
                    }
                }
            }
        }

        private void ExplodeObject(GameObject obj)
        {
            // DONE!
#if ENABLE_CRACK_AND_EXPLODE
        Exploder.CrackObject(obj, OnCracked);
#else
            Exploder.ExplodeObject(obj, OnExplosion);
#endif
        }

        private void OnExplosion(float time, ExploderObject.ExplosionState state)
        {
//            Debug.Log("OnExplosion: " + time + " " + state);
        }

        private void OnCracked(float time, ExploderObject.ExplosionState state)
        {
//            Debug.Log("OnCracked: " + time + " " + state);
        }

        private void ExplodeAfterCrack(GameObject obj)
        {
#if ENABLE_CRACK_AND_EXPLODE
        Exploder.ExplodeCracked(obj, OnExplosion);
#endif
        }

        private void OnGUI()
        {
            if (GUI.Button(new Rect(10, 10, 100, 30), "Reset"))
            {
                if (!Exploder.DestroyOriginalObject)
                {
                    foreach (var destroyableObject in DestroyableObjects)
                    {
                        ExploderUtils.SetActiveRecursively(destroyableObject, true);
                    }
                    ExploderUtils.SetActive(Exploder.gameObject, true);
                }
            }

#if TEST_SCENE_LOAD
            if (GUI.Button(new Rect(10, 50, 100, 30), "NextScene"))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            }
#endif
        }
    }
}
