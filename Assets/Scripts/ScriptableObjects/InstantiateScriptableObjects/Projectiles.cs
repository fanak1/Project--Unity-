using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    public GameObject source;
    [SerializeField] private ProjectileAttribute projectileAttribute;//For custiomize projectile
    public ProjectileBase prefabs; //Monobehaviour 

    private Queue<ProjectileBase> bulletPooling = new Queue<ProjectileBase>();

    private ProjectileBase Create(ProjectileBase prefabs, UnitBase source, int bulletIndex, Vector3 position, Vector3 destination) {
        prefabs.SetProjectileAttribute(projectileAttribute);
        prefabs.SetSourceAndDestination(source, position, destination);
        prefabs.bulletIndex = bulletIndex;
        var p = Instantiate(prefabs, source.transform.position, Quaternion.identity);
        p.OnDisable += ReturnToPool;
        return p;
    }

    private ProjectileBase ShootProjectile(UnitBase source, int bulletIndex, Vector3 position, Vector3 destination) {
        ProjectileBase p;
        if(bulletPooling.Count <= 0) {
            p = Create(prefabs, source, bulletIndex, position, destination);
        } else {
            p = bulletPooling.Dequeue();
            p.SetSourceAndDestination(source, position, destination);
            p.bulletIndex = bulletIndex;
            p.gameObject.SetActive(true);
        }
        return p;
    }

    public void ReturnToPool(ProjectileBase p) {
        bulletPooling.Enqueue(p);
    }

    public void Shoot(UnitBase source, Vector3 position, Vector3 destination) { //Function to fire projectile
        for (int i = 0; i < projectileAttribute.numberOfBullet; i++) {
            var p = ShootProjectile(source, i, position, destination);
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

    public ProjectileAttribute GetAttribute() {
        return this.projectileAttribute;
    }

}
