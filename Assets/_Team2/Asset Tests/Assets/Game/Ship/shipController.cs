using UnityEngine;
using System.Collections;

public class shipController : MonoBehaviour
{
    public float movementSpeedKey = 5f;
    public int hitpoints = 5;
    public GameObject shotPrefab;
    public GameObject explosionPrefab;
    private hitpointsCounter hitpointsCount;
	
	void Awake() { hitpointsCount = FindObjectOfType<hitpointsCounter>(); }
    void Start() { hitpointsCount.updateCounter(hitpoints); }

	void Update ()
    {
        if(!gameManager.pause)
        {
            switch (gameManager.selectedControl)
            {
                case gameManager.controlType.MOUSE:
                    movementMouse();
                    if (Input.GetMouseButtonDown(0)) makeShot();
                    break;

                case gameManager.controlType.KEYBOARD_ARROWS:
                    if (Input.GetKey(KeyCode.LeftArrow)) movementKeyboard(-movementSpeedKey);
                    else if (Input.GetKey(KeyCode.RightArrow)) movementKeyboard(movementSpeedKey);
                    if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl)) makeShot();
                    break;

                case gameManager.controlType.KEYBOARD_WSAD:
                    if (Input.GetKey(KeyCode.A)) movementKeyboard(-movementSpeedKey);
                    else if (Input.GetKey(KeyCode.D)) movementKeyboard(movementSpeedKey);
                    if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl)) makeShot();
                    break;
            }
        }
        //Turn off pause
        else
        {
            switch (gameManager.selectedControl)
            {
                case gameManager.controlType.MOUSE:
                    if (Input.GetMouseButtonDown(0))
                    {
                        gameManager.pause = false;
                        GameObject.FindGameObjectWithTag("Game_pauseTxt").SetActive(false);
                    }
                    break;

                default:
                    if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
                    {
                        gameManager.pause = false;
                        GameObject.FindGameObjectWithTag("Game_pauseTxt").SetActive(false);
                    }
                    break;
            }
        }
	}

    public void destroyShip()
    {
        GameObject obj = Instantiate(explosionPrefab);
        obj.GetComponent<explosionController>().setPosition(transform.position);
        --hitpoints;
        hitpointsCount.updateCounter(hitpoints);
        if (hitpoints == 0)
        {
            Destroy(gameObject);
            gameManager.stopGame();
        }     
    }

    private void movementMouse()
    {
        Vector3 pos = new Vector3(Input.mousePosition.x, transform.position.y);
        transform.position = Camera.main.ScreenToWorldPoint(pos);
        transform.position = new Vector3(transform.position.x, pos.y, pos.z);
    }

    private void movementKeyboard(float direction)
    {
        transform.position = new Vector3(transform.position.x + direction * Time.deltaTime, transform.position.y, transform.position.z);
    }

    private void makeShot()
    {
        GameObject obj = Instantiate(shotPrefab);
        obj.GetComponent<shotController>().setShot(transform.position.x);
    }
}
