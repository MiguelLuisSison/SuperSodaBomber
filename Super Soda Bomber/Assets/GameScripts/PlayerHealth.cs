using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperSodaBomber.Events;

/*
Player Health
    Manages the health behavior of the player.
    It also triggers the damage and death of the player.
*/

public class PlayerHealth : MonoBehaviour
{
    [SerializeField][Range(0, 10)] private int health = 3;
    [SerializeField] private VoidEvent onPlayerDamage;
    [SerializeField] private VoidEvent onPlayerDeath;
    private GameObject player;

    void Awake(){
        player = gameObject;
    }

    void Damage(){
        --health;
        onPlayerDamage.Raise();
        GameplayScript.SetHpUI(health);
        if (health <= 0){
            OnPlayerDeath();
        }
    }

    void OnPlayerDeath(){
        onPlayerDeath.Raise();
        GameplayScript.current.GameOver();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(player.transform.position.y < 0)
            OnPlayerDeath();
    }

    //triggers when player touches an enemy
	private void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.layer == 11){
			Damage();
		}
	}
}
