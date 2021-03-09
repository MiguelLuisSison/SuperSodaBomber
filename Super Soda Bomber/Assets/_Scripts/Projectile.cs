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
    //selects what kind of projectile is it to change the properties
    private enum Type{
        Bomb, Pistol, Cluster, Shotgun
    }
    [SerializeField]
    private Type type;

    //projectile attributes
    private float throwX = 5f;
    private float spin = 200f;
    private bool willExplode = true;
    public Rigidbody2D rigid;

    //determines on what destroys the projectile
    [SerializeField] private LayerMask layersToCollide;

    //particle system (explosion)
    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        //gets the projectile property according to its type
        switch(type){
            case (Type.Bomb):
                SetBomb();
                break;

            case (Type.Pistol):
                SetPistol();
                break;

            case (Type.Cluster):
                SetCluster();
                break;

            case (Type.Shotgun):
                SetShotgun();
                break;
        }

        //central throwing attributes
        rigid.velocity = transform.right * throwX;
        rigid.AddTorque(spin);
        

    }

    //projectile properties

    void SetBomb(){
        float throwY = 200f;
        rigid.gravityScale = 1;
        rigid.AddForce(new Vector2(0f, throwY));
    }

    void SetPistol(){
        rigid.gravityScale = 0;
        willExplode = false;
    }

    //empty classes (for now)
    void SetCluster(){
        
    }

    void SetShotgun(){

    }

    void OnTriggerEnter2D(Collider2D col){
        //detects whether if the projectile collides with the map or the enemy
        if ((layersToCollide.value & 1 << col.gameObject.layer) != 0){
            //if it collides, activate the particle effect and then destroy the Soda Bomb.
            if(willExplode) Explode();

            //if the bomb has direct contact with the enemy, damage the enemy.
            if(col.gameObject.tag == "Enemy"){
                var enemyScript = col.gameObject.GetComponent<Enemy>();
                enemyScript.Damage(25);
            }
            Debug.Log(col.gameObject.tag);
            Destroy(gameObject);
        }

    }

    void Explode(){
        //creates an explosion fx
        Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
    }

}