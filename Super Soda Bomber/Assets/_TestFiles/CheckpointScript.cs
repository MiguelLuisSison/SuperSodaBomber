using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{

    public bool isTouched;
    SpriteRenderer sRenderer;
    Sprite checkActiveImg;


    // Start is called before the first frame update
    void Start()
    {
        sRenderer = gameObject.GetComponent<SpriteRenderer>();
        checkActiveImg = Resources.Load<Sprite>("Gameplay/check_active");
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
