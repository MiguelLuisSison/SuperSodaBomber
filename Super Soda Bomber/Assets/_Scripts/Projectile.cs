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

    //the selected property
    [SerializeField]
    private Type type;

    //projectile attributes
    private float throwX = 3f;
    private float throwMovingMultiplier = 2.5f;
    private float spin = 200f;
    private bool willExplode = true;
    private bool playerMoving;
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
            willExplode = false;
        }
        else {
            s_Projectile = gameObject.AddComponent<SodaBomb>();
        }
    }

    void Start(){
        //central throwing attributes
        s_Projectile.Set(throwX, spin, rigid);

        //set velocity
        //if player is moving, multiply the throwMoving factor
        Debug.Log(playerMoving);
        rigid.velocity = transform.right * throwX * (playerMoving ? throwMovingMultiplier : 1);
        rigid.AddTorque(spin);
    }

    void OnTriggerEnter2D(Collider2D col){
        //detects whether if the projectile collides with the map or the enemy
        if ((layersToCollide.value & 1 << col.gameObject.layer) != 0){
            //if it collides, activate the particle effect and then destroy the Soda Bomb.
            if(willExplode) s_Projectile.Explode(explosion, gameObject);
            Destroy(gameObject);
        }

    }

    public void SetPlayerMoving(bool moving){
        playerMoving = moving;
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

public class SodaBomb: PublicScripts, ISetProjectileProperties{

    //optional variables
    public float throwY = 250f;
    public float blastRadius = 1.5f;

    //sets the SodaBomb's properties
    public void Set(float throwX, float spin, Rigidbody2D rigid){
        rigid.gravityScale = 1;
        rigid.AddForce(new Vector2(0f, this.throwY));
    }

    public void Explode(GameObject explosion, GameObject gameObject){
        //sets a circlecast for blast damage
        var g_Collider = gameObject.GetComponent<BoxCollider2D>();
		Collider2D[] colliders = Physics2D.OverlapCircleAll(gameObject.transform.position, blastRadius);

        if(colliders.Length != 0){
            for(int i = 0; i< colliders.Length; ++i){
                if(colliders[i].gameObject.tag == "Enemy"){
                    //gets the distance between the enemy and the bomb
                    float distance = colliders[i].Distance(g_Collider).distance;
                    var enemyScript = colliders[i].gameObject.GetComponent<Enemy>();
                    //damage the enemy
                    enemyScript.Damage(getDamage(Mathf.Abs(distance)));
                    

                }
            }
        }

        Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
    }

    //fetches the damage according to intensity
    public float getDamage(float radius){
        //inverts the value (lower, the better)
        radius = blastRadius - radius;

        //gets the intensity (0% - 100%)
        float intensity = Mathf.RoundToInt((radius/blastRadius)*100);
        
        if (intensity < 20) {
            GameplayScript.current.AddScore(projScores["sodaBomb_s"]);
            return projDamage["sodaBomb_min"];
        }
        else if (intensity < 75) {
            GameplayScript.current.AddScore(projScores["sodaBomb_m"]);
            return Mathf.RoundToInt(projDamage["sodaBomb_max"]/2);
        }

        //if distance is <= 75% intensity (direct hit)
        GameplayScript.current.AddScore(projScores["sodaBomb_l"]);
        return projDamage["sodaBomb_max"];

    }
}


/*
    Pistol (Fizztol)
        A projectile that fires on a straight line.
        It attacks enemy on contact and doesn't explode.
*/

public class Pistol: MonoBehaviour, ISetProjectileProperties{

    //sets the SodaBomb's properties
    public void Set(float throwX, float spin, Rigidbody2D rigid){
        rigid.gravityScale = 0;
    }

    public void Explode(GameObject explosion, GameObject gameObject){}

}