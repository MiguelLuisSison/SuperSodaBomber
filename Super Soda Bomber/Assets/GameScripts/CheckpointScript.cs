using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
CheckpointScript
    Responsible for the behaviors of the checkpoint
*/

public class CheckpointScript : MonoBehaviour
{

    public bool isTouched;
    SpriteRenderer sRenderer;
    Sprite checkActiveImg;

    void Awake(){
        checkActiveImg = Resources.Load<Sprite>("Gameplay/check_active");
        sRenderer = gameObject.GetComponent<SpriteRenderer>();

    }

    // Changes the sprite of the image if it's touched
    public void ChangeState(){
        sRenderer.sprite = checkActiveImg;
        isTouched = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
