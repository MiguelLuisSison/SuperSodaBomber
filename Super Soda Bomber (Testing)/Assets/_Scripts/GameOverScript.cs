using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    private int score_value = 0;
    public GameObject GameOverPrompt;
    public GameObject QuitBox;
    // Start is called before the first frame update
    public void RestartLevel()
    {
        SceneManager.LoadScene(sceneName: "Level1_Beach");
    }

    public void QuitReturnToMenu()
    {
        SceneManager.LoadScene(sceneName: "MainMenuScene");
    }

}
