using UnityEngine;
using System.Collections;

public class enemyManager : MonoBehaviour
{
    public float movementSpeed = 1f;
    public float speedChanger = 0.16f;
    public float downSpeed = 6f;
    public static bool triggered = false;
    public static bool canMoveDown = true;
    public GameObject[] attackTargets;
    public GameObject[] enemiesLinePrefab;

    private float timeToShot;
    private float timeToAttack;

    void Start()
    {
        timeToShot = Random.Range(1, 2);
        timeToAttack = 15f;
    }

    void Update()
    {
        if (!gameManager.pause)
        {
            randomShot();
            randomAttack();

            if (countEnemies() == 0) loadNewEnemies();
        }
    }

    public void moveDown()
    {
        if(!triggered)
        {
            movementSpeed = -movementSpeed;
            triggered = true;
        }

        if(canMoveDown)
        {
            foreach (Transform line in transform)
            {
                foreach (Transform enemy in line.transform)
                {
                    if(enemy.GetComponent<enemyController>().inAttack) enemy.GetComponent<enemyController>().originPosition = new Vector3(enemy.GetComponent<enemyController>().originPosition.x, enemy.GetComponent<enemyController>().originPosition.y - downSpeed * Time.deltaTime, enemy.GetComponent<enemyController>().originPosition.z);
                    else enemy.transform.position = new Vector3(enemy.transform.position.x, enemy.transform.position.y - downSpeed * Time.deltaTime, enemy.transform.position.z);
                }
            }
        }
    }

    public Vector3 getTarget(int index)
    {
        if(index >= attackTargets.Length)
        {
            Debug.LogError("Target out of range.");
            return new Vector3(0, 0, 0);
        }
        else return attackTargets[index].transform.position;
    }

    private enemyController getRandomEnemy()
    {
        int selectedLine = Random.Range(0, transform.childCount);

        if (transform.GetChild(selectedLine).childCount > 1)
        {
            return transform.GetChild(selectedLine).transform.GetChild(Random.Range(0, transform.GetChild(selectedLine).transform.childCount)).GetComponent<enemyController>();
        }
        else
        {
            Debug.LogWarning("Unable to select enemy.");
            return null;
        }
    }

    private int countEnemies()
    {
        int buffer = 0;
        foreach (Transform line in transform) foreach (Transform enemy in line.transform) ++buffer;
        return buffer;
    }

    private void randomShot()
    {
        if (timeToShot < 0)
        {
            timeToShot = Random.Range(0.1f, 0.8f);

            enemyController buffer = getRandomEnemy();
            if (buffer != null) buffer.shot();
        }
        else timeToShot -= Time.deltaTime;
    }

    private void randomAttack()
    {
        if (timeToAttack < 0)
        {
            timeToAttack = 15f;

            enemyController buffer = getRandomEnemy();
            if (buffer != null) buffer.attack(movementSpeed);
        }
        else timeToAttack -= Time.deltaTime;
    }

    private void loadNewEnemies()
    {
        for (int i = 5; i >= 0; --i) Destroy(transform.GetChild(i).gameObject);
        for (int i = 0; i < 6; ++i)
        {
            GameObject obj = Instantiate(enemiesLinePrefab[i]);
            obj.transform.SetParent(transform);
        }

        triggered = false;
        canMoveDown = true;
    }
}
