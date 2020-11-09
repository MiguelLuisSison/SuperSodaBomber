using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuScript : MonoBehaviour
{
    public GameObject OptionsBox;

    // Variable Configs
    private float masterVol, musicVol, sfxVol, voice;
    private Text label;
    public Slider mastersl, musicsl, sfxsl, voicesl;
    // Start is called before the first frame update

    void Start()
    {
        OptionsBox.SetActive(false);
        masterVol = 0;
        musicVol = 0;
        sfxVol = 0;
    }

    // setting the vars when slider changed
    public void SetMasterVol(){
        Debug.Log("Master Volume: " + mastersl.value);
        masterVol = mastersl.value;
    }

    public void SetMusicVol(){
        Debug.Log("Music Volume: " + musicsl.value);
        masterVol = musicsl.value;
    }

    public void SetSFXVol(){
        Debug.Log("SFX Volume: " + sfxsl.value);
        masterVol = sfxsl.value;
    }

    public void SetVoice(){
        Debug.Log("Voice Toggle: " + voicesl.value);
        masterVol = voicesl.value;
    }

    //gets the prefs
    public void GetVol(){
        PlayerPrefs.SetFloat("Master", mastersl.value);
        PlayerPrefs.SetFloat("Music", musicsl.value);
        PlayerPrefs.SetFloat("SFX", sfxsl.value);
        PlayerPrefs.SetFloat("Voice", voicesl.value);

    }

    // Prompt Visibility
    public void Options()
    {
        OptionsBox.SetActive(true);
    }

    public void ReturnOptions()
    {
        OptionsBox.SetActive(false);
    }
}
