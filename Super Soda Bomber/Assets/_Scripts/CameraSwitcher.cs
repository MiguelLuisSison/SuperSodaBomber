using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    private Animator cameraAnimator;
    private bool cameraAtMain = true;

    void Awake(){
        cameraAnimator = GetComponent<Animator>();
    }
    
    public void SwitchCameraState(){
        Debug.Log("camera switched");
        if (cameraAtMain)
            cameraAnimator.Play("cutscene_camera");
        else
            cameraAnimator.Play("main_camera");

        cameraAtMain = !cameraAtMain;
    }
}
