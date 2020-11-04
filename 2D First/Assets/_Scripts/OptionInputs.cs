using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionInputs : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float masterVol;
    private Text label;
    public Slider sl;

    void Start(){
        masterVol = 0;
    }

    public void SetVol(){
        Debug.Log(sl.value);
        masterVol = sl.value;
    }
    
    public void GetVol(){

        PlayerPrefs.SetFloat("Master", sl.value);
    }
}
