using System.Collections.Generic;
using System.Collections;
using UnityEngine;

/*
Enemy
    Responsible for handling the enemy properties
    and its behaviour.

    Things are needed to improve:
        Make sure that the script is flexible for all types of
        milkings. Except for Milcher. He'll have his own script

    Milkings:
        Shooter Milking
        Roller Milkling
*/

namespace SuperSodaBomber.Enemies{
    //base class template for milklings. all milkling classes will use this
    public abstract class BaseEnemy: MonoBehaviour, IEnemyOuter{
        //list of phase classes that are inside the BaseEnemy
        protected Dictionary<EnemyPhase, IEnemyInner> phaseDict = new Dictionary<EnemyPhase, IEnemyInner>();

        protected IEnemyInner chosenPhase;

        //enemies always start at wandering phase
        protected EnemyState currentState = EnemyState.Wander;
        //player position to track
        protected Vector3 playerPos;
        
        //data needed from scriptable object
        protected float spotRadius, attackRadius, attackRate;
        protected float attackRateMultiplier, healthMultiplier;
        protected bool facingRight;

        //configures data
        public virtual void Init(Enemy_ScriptObject scriptObject, Transform attackSource, EnemyPhase phase){
            spotRadius = scriptObject.spotRadius;
            attackRadius = scriptObject.attackRadius;
            facingRight = scriptObject.facingRight;
            attackRate = scriptObject.attackRate;

            if (phase == EnemyPhase.Phase2)
                attackRate /= scriptObject.attackRateMultiplier;
        }

        //flips the character if it's within the firing radius
        public void Flip(){
            //if the player:
                //is at the right and the enemy is facing left
                //is at the left and the enemy is facing right
            if(facingRight && (playerPos.x < transform.position.x) ||
            !facingRight && playerPos.x > transform.position.x){

                //flip it
                facingRight = !facingRight;

                //Unlike the player, the enemy has a different shape of collider.
                transform.localScale = new Vector3(transform.localScale.x*-1, 1f, 1f);
            }
        }

        //returns true if the enemy is within the radius
        protected bool FindTarget(float radius){
            playerPos = PlayerMovement.playerPos;
            return (Vector3.Distance(transform.position, playerPos)  < radius);
        }

        //calls the state with the chosen phase
        public void InvokeState(){
            chosenPhase.CallState();
        }

        //nested class for phases, since it will have different behavior
        public abstract class BaseInnerEnemy: IEnemyInner{
            //this is where the behavior takes place
            public abstract void CallState();
        }
    }

//ENEMY CLASSES

/*
    Shooter Milkling
        An enemy that shoots a milk projectile to the player.
*/

    public class Shooter: BaseEnemy{
        protected GameObject projectilePrefab;
        protected Transform attackSource;

        public override void Init(Enemy_ScriptObject scriptObject, Transform attackSource, EnemyPhase phase)
        {
            base.Init(scriptObject, attackSource, phase);

            //add the phase sub-classes at the dictionary
            phaseDict.Add(EnemyPhase.Phase1, new Phase1(this));
            phaseDict.Add(EnemyPhase.Phase2, new Phase2(this));

            chosenPhase = phaseDict[phase];
            projectilePrefab = scriptObject.projectilePrefab;
            this.attackSource = attackSource;
        }

        protected void FireProjectile(){
            //get the angle by getting the target direction
            Vector3 targetDir = playerPos - attackSource.position;
            //use trigonometry to get the angle
            //then convert (y/x) rad to degrees
            float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;

            //convert to quaternion (datatype for rotations)
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

            //spawn the projectile
            Instantiate(projectilePrefab, attackSource.position, q);
        }

        protected void InvokeCoroutine(IEnumerator coro){
            StartCoroutine(coro);
        }

        /// <summary>
        /// Shooter fires slowly
        /// </summary>
        public class Phase1: BaseInnerEnemy{
            //outer class to access the functions
            private Shooter outer;
            private float shootTime;

            public Phase1(Shooter o){
                outer = o;
                shootTime = Time.time;
            }

            public override void CallState(){
                switch (outer.currentState){
                    case EnemyState.Wander:
                        //find if the player is within the range
                        if (outer.FindTarget(outer.spotRadius))
                            outer.currentState = EnemyState.Attack;                
                        break;
                    case EnemyState.Attack:
                        //if it's wihtin attacking range and it's within fire rate
                        if (outer.FindTarget(outer.attackRadius) && Time.time > shootTime){
                            //attack
                            shootTime = Time.time + outer.attackRate;
                            outer.FireProjectile();
                        }
                        //switch to wander if it's outside spot radius
                        else if (!outer.FindTarget(outer.spotRadius))
                            outer.currentState = EnemyState.Wander;

                        outer.Flip();
                        break;
                }
            }
        }

        /// <summary>
        /// Shooter fires on a burst
        /// </summary>
        public class Phase2: BaseInnerEnemy{
            private Shooter outer;
            private float shootTime;
            private float burstTime = 0.05f;
            private int shootAmount = 3;

            public Phase2(Shooter o){
                outer = o;
            }

            public override void CallState(){
                switch (outer.currentState){
                    case EnemyState.Wander:
                        //find if the player is within the range
                        if (outer.FindTarget(outer.spotRadius))
                            outer.currentState = EnemyState.Attack;                
                        break;
                    case EnemyState.Attack:
                        if (outer.FindTarget(outer.attackRadius) && Time.time > shootTime){
                            //attack
                            shootTime = Time.time + outer.attackRate;
                            outer.InvokeCoroutine(Burst());
                        }
                        else if (!outer.FindTarget(outer.spotRadius))
                            outer.currentState = EnemyState.Wander;

                        outer.Flip();
                        break;
                }
            }

            private IEnumerator Burst(){
                for (int i = 0; i < shootAmount; i++)
                {
                    outer.FireProjectile();
                    yield return new WaitForSeconds(burstTime);                    
                }
            }
        }
    }

    public class Roller: BaseEnemy{

        public override void Init(Enemy_ScriptObject scriptObject, Transform attackSource, EnemyPhase phase)
        {
            base.Init(scriptObject, attackSource, phase);

            //add the phase sub-classes at the dictionary
            phaseDict.Add(EnemyPhase.Phase1, new Phase1(this));
            phaseDict.Add(EnemyPhase.Phase2, new Phase2(this));

            chosenPhase = phaseDict[phase];
        }

        protected void InvokeCoroutine(IEnumerator coro){
            StartCoroutine(coro);
        }

        /// <summary>
        /// Shooter fires slowly
        /// </summary>
        public class Phase1: BaseInnerEnemy{
            //outer class to access the functions
            private Roller outer;
            private float shootTime;

            public Phase1(Roller o){
                outer = o;
                shootTime = Time.time;
            }

            public override void CallState(){
                switch (outer.currentState){
                    case EnemyState.Wander:
                        //find if the player is within the range
                        if (outer.FindTarget(outer.spotRadius))
                            outer.currentState = EnemyState.Attack;                
                        break;
                    case EnemyState.Attack:
                        //if it's wihtin attacking range and it's within fire rate
                        if (outer.FindTarget(outer.attackRadius) && Time.time > shootTime){
                            //attack
                            shootTime = Time.time + outer.attackRate;
                        }
                        //switch to wander if it's outside spot radius
                        else if (!outer.FindTarget(outer.spotRadius))
                            outer.currentState = EnemyState.Wander;

                        outer.Flip();
                        break;
                }
            }
        }

        /// <summary>
        /// Shooter fires on a burst
        /// </summary>
        public class Phase2: BaseInnerEnemy{
            private Roller outer;
            private float shootTime;

            public Phase2(Roller o){
                outer = o;
            }

            public override void CallState(){
                switch (outer.currentState){
                    case EnemyState.Wander:
                        //find if the player is within the range
                        if (outer.FindTarget(outer.spotRadius))
                            outer.currentState = EnemyState.Attack;                
                        break;
                    case EnemyState.Attack:
                        if (outer.FindTarget(outer.attackRadius) && Time.time > shootTime){
                            //attack
                            shootTime = Time.time + outer.attackRate;
                        }
                        else if (!outer.FindTarget(outer.spotRadius))
                            outer.currentState = EnemyState.Wander;

                        outer.Flip();
                        break;
                }
            }
        }
    }
}