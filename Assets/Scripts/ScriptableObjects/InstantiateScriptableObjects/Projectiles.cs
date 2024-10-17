using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    public GameObject source;
    [SerializeField] private ProjectileAttribute projectileAttribute; //For custiomize projectile
    public ProjectileBase prefabs; //Monobehaviour 

    public void Shoot(UnitBase source, Vector3 position, Vector3 destination) { //Function to fire projectile
        SetStatForProjectile();
        prefabs.SetSourceAndDestination(source, position, destination);
        for (int i = 0; i < projectileAttribute.numberOfBullet; i++) {
            prefabs.bulletIndex = i;
            Instantiate(prefabs, source.transform.position, Quaternion.identity);
        }
    }

    public void SetStatForProjectile(ProjectileAttribute projectileAttribute) { //Function to set stat of projectile manually
        this.projectileAttribute = projectileAttribute;
        prefabs.SetProjectileAttribute(projectileAttribute);
    }

    public void SetStatForProjectile() { //Function to set stat of projectile on default
        prefabs.SetProjectileAttribute(projectileAttribute);
    }

    public bool ShootInterval(float time) { //Interval each shoot time of this Projectile
        return time > prefabs._projectileAttribute.interval;
    }

    public void Init(UnitBase unitBase) {
        this.gameObject.transform.parent = unitBase.transform;
        this.source = unitBase.gameObject;
    }

    public void Init(ProjectileAttribute projectileAttribute, ProjectileBase prefabs) {
        this.projectileAttribute = projectileAttribute;
        this.prefabs = prefabs;
    }

}
