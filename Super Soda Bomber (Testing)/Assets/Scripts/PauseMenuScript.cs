using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public GameObject PauseMenuBox;
    public GameObject QuitBox;
    // Start is called before the first frame update
    public void Resume()
    {
        PauseMenuBox.SetActive(false);
    }

    public void ReloadCheckpoint()
    {
        
    }
    public void QuitConfirmation()
    {
        QuitBox.SetActive(true);
    }

    public void QuitToMenu_Yes()
    {
        SceneManager.LoadScene(sceneName: "MainMenuScene");
    }

    public void QuitToMenu_No()
    {
        QuitBox.SetActive(false);
    }
}
