using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
PlayerAttack
    Used to trigger the attacking of the player
    and how the weapon fires

    Things are needed to improve:
        The script is hard-coded. It only provides attack
        to a fixed projectile with a fixed component/perk.

        This script/other scripts are needed to be flexible
        for projectiles/weapons with different perk and
        property.

        Components that are needed to be flexible with:
            Chosen Bomb/Weapon
            Perk
            Explosion

            Different behaviours caused by a perk (i.e. cluster bomb)
*/

public class PlayerAttack : PublicScripts
{
    //Attacking source of the player. This is where the projectile comes from
    public Transform attackSource;

    //weapon prefab (fix this to make it more flexible)
    public GameObject projectilePrefab;

    private float fireRate;
    private float attackTime;

    // Start is called before the first frame update
    void Awake()
    {
        // Gets GameplayScript components

        //make this flexible to use
        fireRate = fireRates["sodaBomb"];
        attackTime = fireRate;
    }

    public void Attack(bool isMoving, bool attack){
		if (attack && attackTime <= Time.time){
            //creates the projectile
            var projectile = Instantiate(projectilePrefab, attackSource.position, attackSource.rotation);
            var projectileScript = projectile.GetComponent<Projectile>();
            projectileScript.SetPlayerMoving(isMoving);

            //adds the score and updates the attack time
            GameplayScript.current.AddScore(scores["fire"]);
            attackTime = fireRate + Time.time;
		}
	}
}
