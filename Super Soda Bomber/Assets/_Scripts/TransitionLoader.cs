using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

/*
TransitionLoader
    Manages the triggering of the animation of the transition.
*/

/*
TODO:
    When player pressed continue/new button:
        - Transition plays (OK)
        - Loads Title Card (OK)

    At title card:
        - Play a loading animation
        - Async load the game scene (OK)
        - Async load the game over scene
        - Async load the stage complete scene
        - Async load the saved data

    Once all are loaded:
        - Finish playing the loading animation
        - Transition out plays (OK)
        
*/

public class TransitionLoader : PublicScripts
{
    public Animator transition;     //transition animator
    public bool fillEndAuto;        //activates the second-half of the transition
    public bool isTitleCard;        //is the scene a title card?
    public string newScene;        //new scene to load (at inspector)

    private AsyncOperation sceneToLoad;     //tracks the scene loading
    private int oldSceneIndex;      //build index of the old scene

    private void Awake()
    {
        if (fillEndAuto)
            transition.SetTrigger("end");

        //automatically call the load level if it's a title card
        if (isTitleCard && newScene != null){
            StartCoroutine(InvokeLoadLevel());
        }
    }

    public void LoadLevel(string scene)
    {
        newScene = scene;

        //loads the scene asyncronously (without having a lag spike)
        sceneToLoad = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);

        //forces the scene to wait for the transition
        sceneToLoad.allowSceneActivation = false;

        if (!isTitleCard)    
            transition.SetTrigger("start");
    }

    private IEnumerator InvokeLoadLevel(){
        yield return new WaitForSeconds(2f);
        LoadLevel(newScene);
        while (sceneToLoad.progress != .9f){
            yield return null;
        }
        transition.SetTrigger("start");

    }

    private IEnumerator WaitForLoadScene(){
        //waits until the scene is loaded in memory
        while (!sceneToLoad.isDone){
            //if it reaches 90%, allow activate the scene
            if (sceneToLoad.progress == .9f && !sceneToLoad.allowSceneActivation){
                //loading animation stop goes here
                sceneToLoad.allowSceneActivation = true;
                yield return new WaitForEndOfFrame();
            }
            else
                yield return null;
        }

        //sets the loaded scene as the main scene
        oldSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(newScene));

        //unload the old scene
        AsyncOperation unloadScene = SceneManager.UnloadSceneAsync(oldSceneIndex);

        //waits until the scene is unloaded.
        while (!unloadScene.isDone){
            yield return null;
        }
    }

    //calls the coroutine (at the animation event)
    public void CueLoadLevel(){
        if (newScene != null)
            StartCoroutine(WaitForLoadScene());
    }
}
