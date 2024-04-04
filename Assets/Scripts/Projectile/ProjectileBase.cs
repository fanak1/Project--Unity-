using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour
{
    public UnitBase source; //Source of projectile

    public Vector3 position; //position to begin firing projectile
    public Vector3 destination; //destination of projectile aim

    public int bulletIndex;

    public ProjectileAttribute _projectileAttribute;

    public void SetProjectileAttribute(ProjectileAttribute projectileAttribute) => _projectileAttribute = projectileAttribute; //Set Projectile Attribute when Instantiate
    public abstract void Trajectory(); //Detemine how projectile flying from source to destination and index of bullet

    public abstract void Damage(); //Detemine the scale of damage depend on source

    public void SetSourceAndDestination(UnitBase source, Vector3 position, Vector3 destination) { //Set Source and Destination when Instantiate
        this.source = source; 
        this.position = position;
        this.destination = destination;
    }

}
