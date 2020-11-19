using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Contains all scripts that mostly used in the game
//Add component at the EventSystem and then use it to the OnClick() button
public class PublicScripts : MonoBehaviour
{

    //Moves the Scene
    public void _Move(string scene){
        SceneManager.LoadScene(sceneName: scene);
    }
    
    //Toggles on/off the prompt
    public void _TogglePrompt(GameObject prompt){
        bool status = prompt.activeInHierarchy;
        prompt.SetActive(!status);
    }
}