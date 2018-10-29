using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class hitpointsCounter : MonoBehaviour
{
    private Text counter;
	
	void Awake () { counter = gameObject.transform.GetChild(0).GetComponent<Text>(); }
	public void updateCounter(int val) { counter.text = val.ToString(); }
}
