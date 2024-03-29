using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableProjectiles : ScriptableObject
{
    [SerializeField]
    public ProjectileAttribute projectileAttribute { private set; get; } //For custiomize projectile

    public ProjectileAttribute baseAttribute => projectileAttribute; //Base attribute

    public ProjectileBase prefabs; //Monobehaviour 
}

public class ProjectileAttribute {
    public float speed; // Speed of projectile
    public float accel; // Acceleration of projectile
    public int numberOfBullet = 1; //Number of bullet per shot
    public int manaSpend; //Number of mana spend per shot
    public float deviation; //Deviation of each bullet per shot
}
