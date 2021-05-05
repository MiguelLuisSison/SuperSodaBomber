using UnityEngine;
using UnityEngine.UI;

/*
GameOverScript
    simply posts the current score
    when game over.
*/

public class GameOverScript : MonoBehaviour
{
    public Text scoreText;

    void Start(){
        scoreText.text = "Score: " + PlayerPrefs.GetInt("CurrentScore", 0).ToString();
    }

}
