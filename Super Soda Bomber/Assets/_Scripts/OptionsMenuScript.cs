using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuScript : MonoBehaviour
{
    public GameObject OptionsBox;

    // Variable Configs
    private Text label;
    public Slider mastersl, musicsl, sfxsl, voicesl;
    // Start is called before the first frame update

    void Start()
    {
        OptionsBox.SetActive(false);
    }

    // setting the vars when slider changed
    public void SetMasterVol(){
        Debug.Log("Master Volume: " + mastersl.value);
        PlayerPrefs.SetFloat("MasterVol", mastersl.value);
    }

    public void SetMusicVol(){
        Debug.Log("Music Volume: " + musicsl.value);
        PlayerPrefs.SetFloat("MusicVol", musicsl.value);
    }

    public void SetSFXVol(){
        Debug.Log("SFX Volume: " + sfxsl.value);
        PlayerPrefs.SetFloat("SFXVol", sfxsl.value);
    }

    public void SetVoice(){
        Debug.Log("Voice Toggle: " + voicesl.value);
        PlayerPrefs.SetFloat("VoiceToggle", voicesl.value);
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
