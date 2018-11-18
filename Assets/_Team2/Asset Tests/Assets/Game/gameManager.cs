using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class gameManager : MonoBehaviour
{
    public enum controlType { MOUSE, KEYBOARD_ARROWS, KEYBOARD_WSAD }
    public static controlType selectedControl = controlType.MOUSE;
    public static bool pause = true;
    public static int points = 0;
    public static int highScore = 0;
    public static GameObject gameOverTxt;

    private static Text highScoreTxt;

    void Awake()
    {
        gameOverTxt = GameObject.FindGameObjectWithTag("Game_overTxt");
        gameOverTxt.SetActive(false);

        if (PlayerPrefs.HasKey("Highscore")) highScore = PlayerPrefs.GetInt("Highscore");
        else PlayerPrefs.SetInt("Highscore", 0);

        highScoreTxt = GameObject.FindGameObjectWithTag("Game_highscoreTxt").transform.GetChild(0).GetComponent<Text>();
        highScoreTxt.text = highScore.ToString();
    }

    //Load main menu after game over
    void Update() { if(gameOverTxt.activeInHierarchy && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))) SceneManager.LoadScene(0); }

    public static void stopGame()
    {
        gameOverTxt.SetActive(true);
        pause = true;

        if (points > highScore)
        {
            gameOverTxt.transform.GetChild(0).gameObject.SetActive(true);
            highScore = points;
            highScoreTxt.text = highScore.ToString();
            PlayerPrefs.SetInt("Highscore", points);
            PlayerPrefs.Save();
        }
        else gameOverTxt.transform.GetChild(0).gameObject.SetActive(false);
    }
}
