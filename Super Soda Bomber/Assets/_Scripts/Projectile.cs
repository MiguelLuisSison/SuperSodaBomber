using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Projectile
    Responsible for handling the projectile properties
    fired by the player

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

public class Projectile : MonoBehaviour
{

    //projectile attributes
    private float throwX = 5f;
    private float throwY = 200f;
    private float spin = 200f;
    public Rigidbody2D rigid;

    //determines on what destroys the projectile
    [SerializeField] private LayerMask layersToCollide;

    //particle system (explosion)
    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        //throwing attribute
        rigid.velocity = transform.right * throwX;
        rigid.AddForce(new Vector2(0f, throwY));
        rigid.AddTorque(spin);

    }

    void OnTriggerEnter2D(Collider2D col){
        //detects whether if the projectile collides with the map or the enemy
        if ((layersToCollide.value & 1 << col.gameObject.layer) != 0){
            //if it collides, activate the particle effect and then destroy the Soda Bomb.
            Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }

}
