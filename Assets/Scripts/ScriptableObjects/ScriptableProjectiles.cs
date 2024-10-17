using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "NewProjectile", menuName = "Projectile")]
public class ScriptableProjectiles : ScriptableObject
{

    [SerializeField] private ProjectileAttribute projectileAttribute; //For custiomize projectile

    public ProjectileCodeName projectileCodeName;

    public ProjectileAttribute baseAttribute => projectileAttribute; //Base attribute
        
    public ProjectileBase prefabs; //Monobehaviour 

    public Projectiles projectiles;


    public Projectiles Create() {
        var go = new GameObject("Projectile");
        var p = go.AddComponent<Projectiles>();
        p.Init(projectileAttribute, prefabs);
        return p;
    }
}

[Serializable]
public struct ProjectileAttribute {
    public float speed; // Speed of projectile
    public float accel; // Acceleration of projectile
    public int numberOfBullet; //Number of bullet per shot
    public int manaSpend; //Number of mana spend per shot
    public float deviation; //Deviation of each bullet per shot
    public float interval; //Interval of each shoot (aka attack speed)
}

[Serializable]
public enum ProjectileCodeName {
    Spread = 0,
    Pierce = 1,
    Rapid = 2
}
