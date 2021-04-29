using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class load_transition : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;

    private int levelIndex;
    public bool fillEndAuto;


    private void Awake()
    {
        if (fillEndAuto)
            transition.SetTrigger("start_end");
    }


    public void LoadNextLevel(string scene)
    {

     StartCoroutine(LoadLevel(scene));


    }
    IEnumerator LoadLevel(string scene)
    {
        transition.SetTrigger("start_next0");


        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(sceneName: scene);



    }
}
