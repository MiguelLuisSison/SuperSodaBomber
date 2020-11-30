using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

//Contains all scripts that mostly used in the game
//Add component at the EventSystem and then use it to the OnClick() button
public class PublicScripts : MonoBehaviour
{
    //list of scores
    public Dictionary<string, int> scores = new Dictionary<string, int>(){
        {"jump", 10},
        {"checkpoint", 125}
    };

    //description constants
    public Dictionary<string,string> descriptions = new Dictionary<string, string>(){
        {"confirmQuit", "Are sure you want to exit the level?"},
        {"confirmCheckpoint", "Are you sure you want to load the last checkpoint?"},
        {"confirmNew", "Are you sure you want to override your saved game data?"}
    };

    [HideInInspector]
    public string savePath;

    //Moves the Scene
    public void _Move(string scene){
        SceneManager.LoadScene(sceneName: scene);
    }
    
    //Toggles on/off the prompt
    public void _TogglePrompt(GameObject prompt){
        bool status = prompt.activeInHierarchy;
        prompt.SetActive(!status);
    }

    //delete data
    public void ClearData(){
        File.Delete(savePath);
    }

    //path dir
    void Awake(){
        savePath = Application.persistentDataPath + "saved_data.soda";
    }
}