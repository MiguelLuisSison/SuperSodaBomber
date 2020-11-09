using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageCompleteScript : MonoBehaviour
{
    public GameObject L1CompletePrompt;
    public GameObject QuitBox;
    public void ContinueGame()
    {

    }

    public void QuitReturnToMenu()
    {
        QuitBox.SetActive(true);
    }

}
