using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Enemy_ScriptObject
    A scriptable object that holds the initial data for the enemies:
        - Shooter
        - Roller
*/

[CreateAssetMenu(fileName = "New Enemy ScriptableObject", menuName = "ScriptableObjects/Enemy")]
public class Enemy_ScriptObject : ScriptableObject
{
    public float health = 100;
    public float movementSpeed;
    public float attackSpeed;
    public GameObject projectilePrefab;
    public EnemyType enemyType;

    public void OnDeath(){
        //insert the events when the enemy dies
    }
}
