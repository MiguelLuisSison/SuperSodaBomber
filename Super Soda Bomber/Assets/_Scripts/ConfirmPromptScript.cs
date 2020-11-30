using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConfirmPromptScript : PublicScripts
{
    public GameplayScript gameplayScript;
    public Text messageTxt;

    //sets choices for text description
    public enum Key{
        confirmQuit, confirmCheckpoint, confirmNew
    }
    public Key key;

    void Awaken(){
        gameObject.SetActive(false);
    }
    
    void Start(){
        //turns off the added component prompt

        //get the description if key is available or messageTxt was inputted.
        if(descriptions.ContainsKey(key.ToString()) && messageTxt != null){
            messageTxt.text = descriptions[key.ToString()];
        }
    }

    //loads the confirm prompt
    public void LoadConfirmPrompt(GameObject prompt){
        if (prompt != null){
            _TogglePrompt(prompt);
        }
        else{
            Debug.Log("LoadConfirmPrompt has a null GameObject!");
        }
    }

    public void RestartLevel(){
        if (gameplayScript != null){
            gameplayScript.Restart();
        }
        else{
            Debug.Log("gameplayScirpt is notconfigured!");
        }
        
    }

}
