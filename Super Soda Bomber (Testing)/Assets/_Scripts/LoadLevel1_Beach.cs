using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadLevel1_Beach : MonoBehaviour
{
    public GameObject LoadCard;
    public GameObject PauseMenuBox;
    public GameObject QuitBox;
    public GameObject L1CompletePrompt;
    public GameObject GameOverPrompt;
    // Start is called before the first frame update
    void Start()
    {
        LoadCard.SetActive(true);
        PauseMenuBox.SetActive(false);
        QuitBox.SetActive(false);
        L1CompletePrompt.SetActive(false);
        GameOverPrompt.SetActive(false);
        PauseMenuBox.SetActive(false);
        QuitBox.SetActive(false);
        //new WaitForSeconds(6);
        //L1CompletePrompt.SetActive(true);
    }

    public void StageCompleted()
    {
        L1CompletePrompt.SetActive(true);
    }
    
}
