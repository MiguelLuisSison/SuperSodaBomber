using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Enemy
    Responsible for handling the enemy properties
    and its behaviour.

    Things are needed to improve:
        Make sure that the script is flexible for all types of
        milkings. Except for Milcher. He'll have his own script

    Milkings:
        Melee Milkling
        Charging Milking
        Ranged Milkling
*/

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health = 100;

    private enum Type{
    Melee, Charging, Ranged
    }

    [SerializeField]
    private Type type;

    // Start is called before the first frame update
    void Start()
    {

    }

    void FixedUpdate()
    {
        
    }

    public void Damage(float hp){
        health -= hp;

        if (health <=0){
            Die();
        }
    }

    //when the enemy rans out of health
    void Die(){
        Destroy(gameObject);
    }
}
