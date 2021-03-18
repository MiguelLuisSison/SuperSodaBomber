using UnityEngine;
using System.Collections;

/*
Projectile
    Responsible for handling the projectile properties
    and its behavior such as handling how the projectile
    explodes
*/

public class ProjectileManager : PublicScripts
{
    //selects what kind of projectile is it to change the properties
    private enum Type{
        Bomb, Pistol, Cluster, smallCluster, Shotgun
    }

    //the selected property
    [SerializeField]
    private Type type;

    //projectile attributes
    private bool playerMoving;          //is the player fires while moving
    public Rigidbody2D rigid;           //rigidbody2D of the projectile prefab
    private Projectile s_Projectile;    //projectile script of the prefab

    //determines on what destroys the projectile
    [SerializeField] private LayerMask layersToCollide;

    //particle system (explosion)
    public GameObject explosion;

    void Awake()
    {
        //sets the projectile according to enum
        switch (type){
            case Type.Pistol:
                s_Projectile = gameObject.AddComponent<Fizztol>();
                break;
            case Type.Cluster:
                s_Projectile = gameObject.AddComponent<BigCluster>();
                break;
            case Type.smallCluster:
                s_Projectile = gameObject.AddComponent<SmallCluster>();
                break;
            case Type.Shotgun:
                // s_Projectile = gameObject.AddComponent<Shotgun>();
                break;
            default:
                s_Projectile = gameObject.AddComponent<SodaBomb>();
                break;
        }
    }

    void Start(){
        //central throwing attributes
        s_Projectile.Init(rigid, playerMoving);

        //activates delayed detonation for some projectiles
        if (GetExplosionType() == explosionType.Delay)
            StartCoroutine(DelayedDetonation());


    }

    void OnTriggerEnter2D(Collider2D col){
        //detects whether if the projectile collides with the map or the enemy
        if ((layersToCollide.value & 1 << col.gameObject.layer) != 0 && (GetExplosionType() != explosionType.Delay)){
            //if it collides, activate the particle effect and then destroy the Bomb projectile.
            ExplodeProjectile(col);
        }
    }

    IEnumerator DelayedDetonation(){
        yield return new WaitForSeconds(UnityEngine.Random.Range(0f, .25f) + t_clusterExplode);
        DetonateProjectile();
    }

    //explodes the projectile without the use of colliders
    public void DetonateProjectile(){
        ExplodeProjectile();
    }

    public void ExplodeProjectile(Collider2D col = null){
        s_Projectile.Explode(col, explosion);
        Destroy(gameObject);
    }

    //getters and setters
    public void SetPlayerMoving(bool moving){
        playerMoving = moving;
    }

    public Projectile.explosionType GetExplosionType(){
        return s_Projectile.selectedType;
    }

    public string GetPName(){
        return s_Projectile.p_name;
    }

}