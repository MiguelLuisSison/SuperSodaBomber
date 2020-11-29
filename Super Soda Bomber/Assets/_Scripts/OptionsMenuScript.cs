using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuScript : MonoBehaviour
{

    // Variable Configs
    public Slider masterSl, musicSl, sfxSl;
    public Button voiceBtn;

    [Header("Voice Toggle Images")]
    [Space(20f)]

    [SerializeField] private Sprite offImg;
    [SerializeField] private Sprite onImg;
    int voiceEnable;
    void Start()
    {
        //automatically sets the value of the slider
        masterSl.value = PlayerPrefs.GetFloat("MasterVol", 0);
        musicSl.value = PlayerPrefs.GetFloat("MusicVol", 0);
        sfxSl.value = PlayerPrefs.GetFloat("SFXVol", 0);

        //automatically sets the value & image of the voice toggle
        voiceEnable = PlayerPrefs.GetInt("Voice", 0);
        if(voiceEnable == 1){voiceBtn.image.sprite = onImg;}
    }

    // setting the vars when slider changed
    public void _SetVol(Slider sl){
        
        if(sl == masterSl){
            PlayerPrefs.SetFloat("MasterVol", sl.value);
        }
        else if(sl == musicSl){
            PlayerPrefs.SetFloat("MusicVol", sl.value);
        }        
        else if(sl == sfxSl){
            PlayerPrefs.SetFloat("SFXVol", sl.value);
        }
    }

    public void _ToggleVoice(){
        //Since there's no setbool, we will make a make-shift one
        voiceEnable = voiceEnable == 1? 0: 1;
        PlayerPrefs.SetInt("Voice", voiceEnable);
        
        if(voiceEnable == 1){
            voiceBtn.image.sprite = onImg;
        }
        else{
            voiceBtn.image.sprite = offImg;
        }
    }
}
