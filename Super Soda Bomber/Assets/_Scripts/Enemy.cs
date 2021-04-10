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
    [SerializeField] private Enemy_ScriptObject scriptObject;
    private float health, movementSpeed, attackSpeed;
    private GameObject projectilePrefab;
    private EnemyType type;

    void Awake()
    {
        health = scriptObject.health;
        movementSpeed = scriptObject.movementSpeed;
        attackSpeed = scriptObject.attackSpeed;
        projectilePrefab = scriptObject.projectilePrefab;
        type = scriptObject.enemyType;
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
        scriptObject.OnDeath();
        Destroy(gameObject);
    }
}
