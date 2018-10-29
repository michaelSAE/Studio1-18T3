using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuUtilities : MonoBehaviour
{
    public void buttonNewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void buttonExitGame()
    {
        Application.Quit();
    }
}
