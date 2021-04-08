using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System;
using UnityEngine;
using UnityEngine.Events;

// Projectile Processor
/// <summary>
/// Automatically determines the projectile type and its component using the prefab.
/// </summary>

public static class ProjectileProcessor{
    /// <summary>
    /// Dictionary containing all of the projectile classes
    /// </summary>
    private static Dictionary<string, Type> projectileDict = new Dictionary<string, Type>();

    //directory of projectile prefabs
    public static string projectilePath = "Prefabs/Weapons/";
    public static PlayerProjectiles projectileType { get; private set; }
    private static string projectileName;

    /// <summary>
    /// Creates a lookup table of projectile classes.
    /// </summary>
    public static void Init(){
        projectileDict.Clear();

        //gets all of the Projectile classes on the current framework
        var assembly = Assembly.GetAssembly(typeof(Projectile));
        
        //filters it with the following conditions:
        //  if a class inherits Projectile
        //  if it's not an abstract class
        
        var allProjectileTypes = assembly.GetTypes()
            .Where(t => typeof(Projectile).IsAssignableFrom(t) && t.IsAbstract == false);

        //loops allProjectileTypes and store it into the dictionary
        foreach (var projType in allProjectileTypes){
            projectileDict.Add(projType.FullName, projType);
        }
    }

    //returns the projectile prefab
    public static GameObject GetPrefab(PlayerProjectiles projType){
        projectileType = projType;
        if (projectileName == "")
            projectileName = null;

        // if the projectile name and projType is empty, call soda bomb as default
        if (projType == PlayerProjectiles.Undefined && projectileName == null){
            projectileName = "Soda Bomb";
            Debug.Log("projectile name is empty! Redirecting to Soda Bomb");
            //retain data if projectileName exists
        }
        else if (projType != PlayerProjectiles.Undefined)
            projectileName = EnumToString(projType);

        Debug.Log($"projectile type @ get prefab: \"{projectileName}\"");

        //loads the prefab using the directory
        return Resources.Load<GameObject>(projectilePath + projectileName) as GameObject;
    }

    //returns a projectile prefab with its corresponding script
    public static Projectile ConfigureComponent(GameObject prefab){
        Type prefabComponent;
        string name = prefab.name.Replace("(Clone)", "");

        try{
            //fetches the component and adds it
            prefabComponent = projectileDict[name];
            return prefab.AddComponent(prefabComponent) as Projectile;

        }
        catch (KeyNotFoundException){
            //key not found
            return prefab.AddComponent<SodaBomb>();
        }
    }

    public static string GetProjectileName(){
        Debug.Log($"getting projectile name: {projectileName}");
        return projectileName;
    }

    public static void SetProjectileName(PlayerProjectiles projType){
        Debug.Log($"setting projectile name: {projType}");
        string projTypeString = EnumToString(projType);
        if (projTypeString != null)
            projectileName = projTypeString;
    }

    private static string EnumToString(PlayerProjectiles p){
        switch (p){
            case PlayerProjectiles.SodaBomb:
                return "Soda Bomb";
            case PlayerProjectiles.Undefined:
                return null;
            default:
                return p.ToString();
        }
    }

}

public static class AbilityProcessor
{
    private static Ability ability;
    private static ActiveAbility activeAbility;
    private static PassiveAbility passiveAbility;
    private static bool isInitialized;

    //handles the cooldown of the ability
    private static Coroutine coroutine;
    
    /// <summary>
    /// Selects and configures the ability to the player according to the inputted key
    /// </summary>
    /// <param name="key">Type of ability</param>
    /// <param name="controller">Player Control</param>
    public static void Fetch(PlayerAbilities key, PlayerMovement controller)
    {
        //NOTE: Fetch() should only be called ONCE.
        if (!isInitialized){
            //small if-else statement to choose the correct ability
            if (key == PlayerAbilities.DoubleJump){
                activeAbility = new DoubleJump();
            }
            else if (key == PlayerAbilities.Dash){
                activeAbility = new Dash();
                controller.flipEvent += activeAbility.OnFlip;
            }
            else if (key == PlayerAbilities.LongJump){
                passiveAbility = new LongJump();
                controller.m_JumpForce = passiveAbility.ApplyPassiveAbility(
                    controller.m_JumpForce);
            }

            //call the init if the active ability is loaded
            activeAbility?.Init(controller.m_AbilityCallEvent);
            isInitialized = true;
        }
    }

    public static float GetCooldown(){
        if (activeAbility != null){
            return activeAbility.cooldown;
        }
        return 0;
    }
}
