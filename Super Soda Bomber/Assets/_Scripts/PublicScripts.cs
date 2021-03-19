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
    //list of non-projectile scores
    protected Dictionary<string, int> scores = new Dictionary<string, int>(){
        {"jump", 10},
        {"checkpoint", 125},
        {"fire", 10}
    };

    /*list of projectile scores
        formatting:
            if it's an explosive, add _s, _m and _l for variating scores
            otherwise, add the name as is
            
            you can find the name of the projectile at Projectile.cs, p_name
    */
    protected Dictionary<string, int> projScores = new Dictionary<string, int>(){
        {"sodaBomb_s", 15},
        {"sodaBomb_m", 30},
        {"sodaBomb_l", 75},
        {"smallCluster_s", 15},
        {"smallCluster_m", 30},
        {"smallCluster_l", 75},
    };

    //description constants
    protected Dictionary<string,string> descriptions = new Dictionary<string, string>(){
        {"checkSave", "Checkpoint Saved!"},
        {"confirmQuit", "Are sure you want to exit the level?"},
        {"confirmCheckpoint", "Are you sure you want to load the last checkpoint?"},
        {"confirmNew", "Are you sure you want to override your saved game data?"}
    };

    //firing rates of the weapons (shows cooldown in secs)
    protected Dictionary <string, float> fireRates = new Dictionary <string, float>(){
        {"sodaBomb", .6f},
        {"pistol", .45f},
        {"bigCluster", 1.2f}

    };

    /*damage matrix for projectiles
        formatting:
            if it's an explosive, add _max and _min for damage range
            otherwise, add the name as is
    */
    protected Dictionary <string, float> projDamage = new Dictionary<string, float>(){
        {"sodaBomb_max", 50f},
        {"sodaBomb_min", 20f},
        {"smallCluster_max", 30f},
        {"smallCluster_min", 10f}

    };

    //explosion types
    public enum explosionType{
        Contact = 0,        //collision triggers explosion (default)
        Detonate,           //player or time triggers explosion
        Delay,              //time triggers explosion
        Instant             //instantly explodes
    }

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