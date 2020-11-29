using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayScript : MonoBehaviour
{
    // Script for TestGameplay
    // Start is called before the first frame update

    /*
    Process:
    When user touches the checkpoint:
        - activated save function
        - change the state of the checkpoint
        - change the image

    When game starts:
        - load the scene and saved files
        - add player states
    */
    //Config Variables
    
    public GameObject scoreTxtObject;

    // //Variables to Save
    private int score = 0;

    void Start()
    {

    }

    //adds score
    public void AddScore(int amount){
        score += amount;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Text scoreTxt = scoreTxtObject.GetComponent<Text>();
        scoreTxt.text = "Score: " + score;
    }
}
