using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
CheckpointScript
    Responsible for the behaviors of the checkpoint
*/

public class CheckpointScript : PublicScripts
{

    private bool isTouched; //used to verify if the checkpoint has been already triggered
    private TextMesh notification;
    private SpriteRenderer sRenderer; //used to render the sprite
    private Sprite checkActiveImg; //image of the checkpoint

    void Awake(){
        checkActiveImg = Resources.Load<Sprite>("Gameplay/check_active");
        sRenderer = gameObject.GetComponent<SpriteRenderer>();
        notification = gameObject.GetComponentInChildren<TextMesh>();
        notification.gameObject.SetActive(false);

    }

    public void ChangeState(){
        //changes the sprite of the image if it's touched
        sRenderer.sprite = checkActiveImg;
        isTouched = true;
        
    }
    
    void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.layer == 8){
            if (!isTouched){
                ChangeState();
                GameplayScript.current.AddScore(scores["checkpoint"]);
                GameplayScript.current.SetCheckpoint(transform.position, gameObject.name);
                Debug.Log("Checkpoint Saved!");
            }
            _TogglePrompt(notification.gameObject);
        }
    }
    void OnTriggerExit2D(Collider2D col){
        if (col.gameObject.layer == 8)
            _TogglePrompt(notification.gameObject);
    }
}
