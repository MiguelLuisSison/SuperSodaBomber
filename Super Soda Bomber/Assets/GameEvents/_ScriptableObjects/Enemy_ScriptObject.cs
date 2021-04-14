using UnityEngine;

/*
Enemy_ScriptObject
    A scriptable object that holds the initial data for the enemies:
        - Shooter
        - Roller
*/
namespace SuperSodaBomber.Enemies{
    [CreateAssetMenu(fileName = "New Enemy ScriptableObject", menuName = "ScriptableObjects/Enemy")]
    public class Enemy_ScriptObject : ScriptableObject
    {
        [Range(50f, 500f)] public float health = 100f;
        [Range(0, 3f)] public float movementSpeed = 0;
        [Range(.01f, 5f)] public float attackRate = 1.5f;

        //attacking range for the enemy
        [Range(.5f, 5f)] public float attackRadius = 1.5f;
        //range where the enemy can look at the enemy
        [Range(1f, 10f)] public float spotRadius = 3f;

        public GameObject projectilePrefab;
        public EnemyType enemyType;
        public EnemyPhase enemyPhase;
        public bool isMoving = false;
        public bool facingRight = true;

        [Header("Phase 2 Modifiers")]
        [Range(1f, 3f)] public float healthMultiplier = 1f;
        [Range(1f, 2f)] public float attackRateMultiplier = 1f;
        public Sprite phase2Sprite;

    }
}
