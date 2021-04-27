using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using SuperSodaBomber.Events;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private VoidEvent onCutsceneEvent;
    private bool fix = false;
    private RuntimeAnimatorController animController;
    private PlayableDirector director;

    // Start is called before the first frame update
    void Start()
    {
        //deactivates the animator controller at the cutscene
        director = GetComponent<PlayableDirector>();
        animController = animator.runtimeAnimatorController;
        animator.runtimeAnimatorController = null;
        onCutsceneEvent.Raise();
    }

    // Update is called once per frame
    void Update()
    {
        //if the cutscene ends, the player can now move.
        if (director.state != PlayState.Playing && !fix){
            fix = true;
            animator.runtimeAnimatorController = animController;
            onCutsceneEvent.Raise();
        }
    }
}
