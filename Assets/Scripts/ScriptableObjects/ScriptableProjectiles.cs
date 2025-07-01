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
    public float scale;

    public static ProjectileAttribute operator +(ProjectileAttribute a, ProjectileAttribute b)
    {
        return new ProjectileAttribute
        {
            speed = Mathf.Max(a.speed + b.speed, 0),
            accel = Mathf.Max(a.accel + b.accel, 0),
            numberOfBullet = Mathf.Max(a.numberOfBullet + b.numberOfBullet, 0),
            manaSpend = Mathf.Max(a.manaSpend + b.manaSpend, 0),
            deviation = Mathf.Max(a.deviation + b.deviation, 0),
            interval = Mathf.Max(a.interval + b.interval, 0),
            scale = Mathf.Max(a.scale + b.scale, 0)
        };
    }

    public static ProjectileAttribute operator *(ProjectileAttribute a, float amount)
    {
        return new ProjectileAttribute
        {
            speed = Mathf.Max(a.speed * amount, 0),
            accel = Mathf.Max(a.accel * amount, 0),
            numberOfBullet = Mathf.Max(a.numberOfBullet * (int)amount, 0),
            manaSpend = Mathf.Max(a.manaSpend * (int)amount, 0),
            deviation = Mathf.Max(a.deviation * amount, 0),
            interval = Mathf.Max(a.interval * amount, 0),
            scale = Mathf.Max(a.scale * amount, 0)
        };
    }
}

[Serializable]
public enum ProjectileCodeName {
    Spread = 0,
    Pierce = 1,
    Rapid = 2
}
