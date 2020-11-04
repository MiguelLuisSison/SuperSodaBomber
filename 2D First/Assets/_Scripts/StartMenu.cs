using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//additional imports
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartPlay()
    {
        SceneManager.LoadScene(sceneName:"Options");
    }

}
