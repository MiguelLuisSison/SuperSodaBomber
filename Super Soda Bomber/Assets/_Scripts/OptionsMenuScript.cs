using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuScript : MonoBehaviour
{

    // Variable Configs
    public Slider mastersl, musicsl, sfxsl;

    void Start()
    {

        //automatically sets the value of the slider
        mastersl.value = PlayerPrefs.GetFloat("MasterVol", 0);
        musicsl.value = PlayerPrefs.GetFloat("MusicVol", 0);
        sfxsl.value = PlayerPrefs.GetFloat("SFXVol", 0);
    }

    // setting the vars when slider changed
    public void _SetVol(Slider sl){
        
        if(sl == mastersl){
            PlayerPrefs.SetFloat("MasterVol", sl.value);
        }
        else if(sl == musicsl){
            PlayerPrefs.SetFloat("MusicVol", sl.value);
        }        
        else if(sl == sfxsl){
            PlayerPrefs.SetFloat("SFXVol", sl.value);
        }
    }
}
