using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionLoader : PublicScripts
{
    public Animator transition;
    public bool fillEndAuto;

    private int levelIndex;
    private string scene;



    private void Awake()
    {
        if (fillEndAuto)
            transition.SetTrigger("end");
    }

    public void LoadNextLevel(string scene)
    {
        this.scene = scene;
        transition.SetTrigger("start");

    }

    public void CueLoadLevel(){
        if (scene != null)
            _Move(scene);
    }
}
