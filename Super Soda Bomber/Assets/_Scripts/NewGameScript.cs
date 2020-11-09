using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewGameScript : MonoBehaviour
{
    public GameObject NewGameBox;
    // Start is called before the first frame update
    void Start()
    {
        NewGameBox.SetActive(false);
    }
    public void NewGame()
    {
        NewGameBox.SetActive(true);
    }

    public void Confirmation_Yes()
    {
        SceneManager.LoadScene(sceneName: "Level1_Beach");
    }

    public void Confirmation_No()
    {
        NewGameBox.SetActive(false);
    }
}
