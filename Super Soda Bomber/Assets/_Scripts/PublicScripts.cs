using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
PublicScripts
    Contains all scripts that are mostly used in the game

    Make sure to add this script at the EventSystem 
    and then use this to the OnClick() buttons if needed.
*/

public class PublicScripts : MonoBehaviour
{
    //list of scores
    public Dictionary<string, int> scores = new Dictionary<string, int>(){
        {"jump", 10},
        {"checkpoint", 125},
        {"fire", 10}
    };

    //description constants
    public Dictionary<string,string> descriptions = new Dictionary<string, string>(){
        {"confirmQuit", "Are sure you want to exit the level?"},
        {"confirmCheckpoint", "Are you sure you want to load the last checkpoint?"},
        {"confirmNew", "Are you sure you want to override your saved game data?"}
    };

    //firing rates of the weapons (shows cooldown in secs)
    public Dictionary <string, float> fireRates = new Dictionary <string, float>(){
        {"sodaBomb", .5f}
    };


    [HideInInspector]
    public string savePath;
    
    //used for save/load processes
    public BinaryFormatter bf = new BinaryFormatter();

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