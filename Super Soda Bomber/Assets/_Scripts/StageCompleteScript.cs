using UnityEngine;
using UnityEngine.UI;

/*
StageCompleteScript
    simply posts the current score
    when stage is complete + save and move stage.
*/

public class StageCompleteScript : MonoBehaviour
{
    public Text scoreText;

    void Start(){
        scoreText.text = "Score: " + PlayerPrefs.GetInt("CurrentScore", 0).ToString();
    }

    void PerkChoose(){
        
    }

}
