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
            Chosen Bomb/Weapon (GOOD)
            Perk
            Explosion (GOOD)

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
    private ISetProjectileProperties s_Projectile;

    //determines on what destroys the projectile
    [SerializeField] private LayerMask layersToCollide;

    //particle system (explosion)
    public GameObject explosion;

    void Awake()
    {
        //sets the projectile according to enum
        if(type == Type.Pistol){
            s_Projectile = gameObject.AddComponent<Pistol>();
        }
        else {
            s_Projectile = gameObject.AddComponent<SodaBomb>();
        }
    }

    void Start(){
        //central throwing attributes
        s_Projectile.Set(throwX, spin, rigid);
        rigid.velocity = transform.right * throwX;
        rigid.AddTorque(spin);
    }

    void OnTriggerEnter2D(Collider2D col){
        //detects whether if the projectile collides with the map or the enemy
        if ((layersToCollide.value & 1 << col.gameObject.layer) != 0){
            //if it collides, activate the particle effect and then destroy the Soda Bomb.
            if(willExplode) s_Projectile.Explode(explosion, gameObject);

            //if the bomb has direct contact with the enemy, damage the enemy.
            if(col.gameObject.tag == "Enemy"){
                var enemyScript = col.gameObject.GetComponent<Enemy>();
                enemyScript.Damage(25);
            }
            Debug.Log(col.gameObject.tag);
            Destroy(gameObject);
        }

    }

}

//interfaces are like templates for classes
//because projectiles have same classes but they have different behavior
public interface ISetProjectileProperties{
    //required function per class
    void Set(float throwX, float spin, Rigidbody2D rigid);
    void Explode(GameObject explosion, GameObject gameObject);
}

// PROJECTILE TYPES
/*
    Soda Bomb
        A projectile that fires on a curve. It explodes
        on contact.

        This is Fizzy's stock weapon.
*/

public class SodaBomb: MonoBehaviour, ISetProjectileProperties{

    //optional variables
    public float throwY = 200f;

    //sets the SodaBomb's properties
    public void Set(float throwX, float spin, Rigidbody2D rigid){
        rigid.gravityScale = 1;
        rigid.AddForce(new Vector2(0f, this.throwY));
    }

    public void Explode(GameObject explosion, GameObject gameObject){
        Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
    }
}

/*
    Pistol (Fizztol)
        A projectile that fires on a straight line.
        It attacks enemy on contact.
*/

public class Pistol: MonoBehaviour, ISetProjectileProperties{

    //sets the SodaBomb's properties
    public void Set(float throwX, float spin, Rigidbody2D rigid){
        rigid.gravityScale = 0;
    }

    public void Explode(GameObject explosion, GameObject gameObject){}

}