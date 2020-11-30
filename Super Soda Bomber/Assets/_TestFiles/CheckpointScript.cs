using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{

    public bool isTouched;
    SpriteRenderer sRenderer;
    Sprite checkActiveImg;

    void Awake(){
        checkActiveImg = Resources.Load<Sprite>("Gameplay/check_active");
        sRenderer = gameObject.GetComponent<SpriteRenderer>();

    }

    // Changes the sprite of this sprite
    public void ChangeState(){
        sRenderer.sprite = checkActiveImg;
        isTouched = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
