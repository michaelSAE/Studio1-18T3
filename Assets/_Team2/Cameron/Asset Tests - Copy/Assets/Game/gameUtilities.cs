using UnityEngine;
using System.Collections;

public class gameUtilities : MonoBehaviour
{
    public enum utilityType { MOVEENEMY, STOPENEMY, DESTROYSHOT }
    public utilityType type;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(type == utilityType.MOVEENEMY && collider.tag == "Game_enemy") FindObjectOfType<enemyManager>().moveDown();
        else if(type == utilityType.DESTROYSHOT && collider.tag == "Game_shot") Destroy(collider.gameObject);
        else if(type == utilityType.STOPENEMY && collider.tag == "Game_enemy" && !collider.GetComponent<enemyController>().inAttack) enemyManager.canMoveDown = false;
    }

    void OnTriggerExit2D(Collider2D collider) { if(type == utilityType.MOVEENEMY && collider.tag == "Game_enemy") enemyManager.triggered = false; }
}
