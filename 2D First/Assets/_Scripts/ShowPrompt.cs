using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//new module import
using UnityEngine.UI;

public class ShowPrompt : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pBox;

    // Update is called once per frame
    public void ExitPrompt(){
        pBox.SetActive(false);
    }
}
