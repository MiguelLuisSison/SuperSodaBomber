using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPowerup{
    void Apply(GameObject player);
}

public interface IDurationPowerup{
    IEnumerator AbilityEffect();
}

/// <summary>
/// Abilities that enhances one of the player's abilities temporarily.
/// </summary>

public class Powerup: MonoBehaviour{
    public enum PowerupType{SugarRush, OneUP}
    public PowerupType key;

    private IPowerup powerupComponent;

    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.layer != 8)
            return;

        var playerControl = col.gameObject.GetComponent<PlayerControl>();
        switch (key){
            case PowerupType.SugarRush:
                playerControl.AddPowerup(new SugarRush());
                break;
            case PowerupType.OneUP:
                playerControl.AddPowerup(new OneUP());
                break;
        }
        
        Debug.Log("powerup:" + key);
        Destroy(gameObject);
    }
}

public class SugarRush: IPowerup, IDurationPowerup
{
    private float abilityDuration = 5;
    private float multiplier = 2;
    private float oldValue;
    private PlayerAttack playerAtk;
    private PlayerControl playerControl;

    public void Apply(GameObject player){
        playerAtk = player.GetComponent<PlayerAttack>();
        playerControl = player.GetComponent<PlayerControl>();

        oldValue = playerAtk.rateMultiplier;

        playerAtk.SetAttackRateMultiplier(oldValue * multiplier);
        Debug.Log("sugar rush has started!");
    }

    /// <summary>Waits for duration and then unapply the ability.</summary>
    public IEnumerator AbilityEffect()
    {
        yield return new WaitForSeconds(abilityDuration);
        Debug.Log("sugar rush has ended!");
        playerAtk.SetAttackRateMultiplier(oldValue);
        playerControl.RemovePowerup(this);
    }
}


public class OneUP: IPowerup{
    public void Apply(GameObject player){
        var playerHealth = player.GetComponent<PlayerHealth>();
        var playerControl = player.GetComponent<PlayerControl>();
        playerHealth.AddHP();
        playerControl.RemovePowerup(this);
    }
}
