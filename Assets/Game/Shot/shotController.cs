using UnityEngine;
using System.Collections;

public class shotController : MonoBehaviour
{
    public enum shotOrigin { PLAYER, ENEMY }
    public shotOrigin origin;
    public float movementSpeed = 10f;
    public int damage = 10;
    private enemyManager manager;

    void Start() { manager = FindObjectOfType<enemyManager>(); }

    void Update()
    {
        Vector3 pos = transform.position;
        if (origin == shotOrigin.PLAYER) transform.position = new Vector3(pos.x, pos.y + movementSpeed * Time.deltaTime, pos.z);
        else transform.position = new Vector3(pos.x, pos.y - movementSpeed * Time.deltaTime, pos.z);
    }

    //setShot for player
    public void setShot(float posX)
    {
        origin = shotOrigin.PLAYER;
        transform.position = new Vector3(posX, -3.532f, 89);
    }

    //setShot for enemies
    public void shot(float posX, float posY)
    {
        origin = shotOrigin.ENEMY;
        transform.position = new Vector3(posX, posY, 89);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "Game_barricade")
        {
            if (damage == 1) Destroy(gameObject);
            else --damage;
            Destroy(col.gameObject);
        }
        else if (col.collider.tag == "Game_player")
        {
            col.gameObject.GetComponent<shipController>().destroyShip();
            Destroy(gameObject);
        }
        else if (col.collider.tag == "Game_enemy")
        {
            if(origin == shotOrigin.PLAYER)
            {
                col.gameObject.GetComponent<enemyController>().destroyEnemy();
                gameManager.points += 10;
                FindObjectOfType<pointsCounter>().updatePoints();
                Destroy(gameObject);

                if (manager.movementSpeed > 0) manager.movementSpeed += manager.speedChanger;
                else manager.movementSpeed -= manager.speedChanger;
            }
        }
        else Debug.LogWarning("Undefined tag: " + col.collider.tag);
    }
}
