using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuScript : MonoBehaviour
{
    public GameObject OptionsBox;
    // Start is called before the first frame update
    void Start()
    {
        OptionsBox.SetActive(false);
    }
    public void Options()
    {
        OptionsBox.SetActive(true);
    }

    public void ReturnOptions()
    {
        OptionsBox.SetActive(false);
    }
}
