using UnityEngine;
using System.Collections;

public class enemyController : MonoBehaviour
{
    public GameObject shotPrefab;
    public GameObject explosionPrefab;
    public bool inAttack = false;
    public Vector3 originPosition;

    private int attackTarget = 0;
    private float shotTimerAttack;
    private float attackSpeed = 0f;
    private enemyManager manager;

    void Start ()
    {
        manager = FindObjectOfType<enemyManager>();
        shotTimerAttack = 1f;
    }
	
	void Update ()
    {
        if(!gameManager.pause)
        {
            if (!inAttack) normalMode();
            else attackMode();
        }
    }

    public void destroyEnemy()
    {
        GameObject obj = Instantiate(explosionPrefab);
        obj.GetComponent<explosionController>().setPosition(transform.position);

        Destroy(gameObject);
    }

    public void shot()
    {
        GameObject obj = Instantiate(shotPrefab);
        obj.GetComponent<shotController>().shot(transform.position.x, transform.position.y);
    }

    public void attack(float movementSpeed)
    {
        inAttack = true;
        attackSpeed = Mathf.Abs(movementSpeed) * (4f / Mathf.Abs(movementSpeed));
    }

    private void normalMode()
    {
        transform.position = new Vector3(transform.position.x + manager.movementSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        originPosition = transform.position;
    }

    private void attackMode()
    {
        attackShot();
        originPosition = new Vector3(originPosition.x + manager.movementSpeed * Time.deltaTime, originPosition.y, originPosition.z);

        if (attackTarget < 6) //Move enemy to target position
        {
            transform.position = Vector3.MoveTowards(transform.position, manager.getTarget(attackTarget), attackSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, manager.getTarget(attackTarget)) < 1f) ++attackTarget;
        }
        else //Move enemy to origin position
        {
            transform.position = Vector3.MoveTowards(transform.position, originPosition, attackSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, originPosition) < 0.2f)
            {
                transform.position = originPosition;
                inAttack = false;
                attackTarget = 0;
                shotTimerAttack = 1f;
            }
        }
    }

    private void attackShot()
    {
        if (shotTimerAttack < 0)
        {
            shot();
            shotTimerAttack = 0.2f;
        }
        else shotTimerAttack -= Time.deltaTime;
    }
}
