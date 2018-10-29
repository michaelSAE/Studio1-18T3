using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class pointsCounter : MonoBehaviour
{
    private Text counter;

    void Awake()
    {
        counter = gameObject.transform.GetChild(0).GetComponent<Text>();
        counter.text = "0";
    }

    public void updatePoints() { counter.text = gameManager.points.ToString(); }
}
