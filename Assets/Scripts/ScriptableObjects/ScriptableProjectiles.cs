using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "NewProjectile", menuName = "Projectile")]
public class ScriptableProjectiles : ScriptableObject
{

    [SerializeField] private ProjectileAttribute projectileAttribute; //For custiomize projectile

    public ProjectileAttribute baseAttribute => projectileAttribute; //Base attribute

    public ProjectileBase prefabs; //Monobehaviour 

    public void Shoot(UnitBase source, Vector3 position, Vector3 destination) { //Function to fire projectile
        SetStatForProjectile();
        prefabs.SetSourceAndDestination(source, position, destination);
        for(int i=0; i<projectileAttribute.numberOfBullet; i++) {
            prefabs.bulletIndex = i;
            Instantiate(prefabs, source.transform.position, Quaternion.identity);
        }
    }

    public void SetStatForProjectile(ProjectileAttribute projectileAttribute) { //Function to set stat of projectile manually
        prefabs.SetProjectileAttribute(projectileAttribute);
    }

    public void SetStatForProjectile() { //Function to set stat of projectile on default
        prefabs.SetProjectileAttribute(projectileAttribute);
    }
}

[Serializable]
public struct ProjectileAttribute {
    public float speed; // Speed of projectile
    public float accel; // Acceleration of projectile
    public int numberOfBullet; //Number of bullet per shot
    public int manaSpend; //Number of mana spend per shot
    public float deviation; //Deviation of each bullet per shot
}
