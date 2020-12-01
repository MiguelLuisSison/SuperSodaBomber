using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageCompleteScript : MonoBehaviour
{
    public Text scoreText;

    void Start(){
        scoreText.text = "Score: " + PlayerPrefs.GetInt("CurrentScore", 0).ToString();
    }

    void PerkChoose(){
        
    }

}
