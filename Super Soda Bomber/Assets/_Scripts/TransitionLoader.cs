using System.Collections.Generic;
using SuperSodaBomber.Events;
using UnityEngine;

/*
TransitionLoader
    Manages the triggering of the animation of the transition.
*/

public class TransitionLoader : MonoBehaviour
{
    //transition animator
    [SerializeField] private Animator transition;   
    public VoidEvent duringTransitionScene;

    [Header("Main Menu to Game Level")]
    [SerializeField] private List<VoidEvent> duringTransitionEvents;
    [SerializeField] private List<VoidEvent> afterTransitionEvents;

    private string status;
    private int index = 0;

    private static bool useMainMenuEvent;
    public static bool UseMainMenuEvents
    {
        get { return useMainMenuEvent; }
        set { useMainMenuEvent = value; }
    }
    
    public void ToggleMainMenuEvents(bool active){
        useMainMenuEvent = active;
    }

    public void FillStart(){
        status = "fill_start";
        transition.Play("fill_start");
        Debug.Log("start called");
    }

    public void FillEnd(){
        status = "fill_end";
        transition.Play("fill_end");
    }

    public void CueTransition(){
        if(status == "fill_start"){
            FillEnd();
        }
        else{
            FillStart();
        }
    }

    public void CueStartDone(){
        Debug.Log("MainMenuUse: " + useMainMenuEvent);
        if (index < duringTransitionEvents.Count && useMainMenuEvent){
            duringTransitionEvents[index]?.Raise();
            FillEnd();
        }
        else if(!useMainMenuEvent){
            duringTransitionScene?.Raise();
        }
    }

    public void CueEndDone(){
        if (useMainMenuEvent){
            if (index < afterTransitionEvents.Count){
                afterTransitionEvents[index]?.Raise();
            }
            index++;
        }
    }
}
