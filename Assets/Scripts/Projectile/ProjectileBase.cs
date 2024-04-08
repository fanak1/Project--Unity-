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

    private bool collided = false; //Check if target Collide with another, if collided, then dont collide with another

    public virtual void Start() {
        collided = false;
    }

    public virtual void Update() {
        Trajectory();
        DestroyOnOverBounds();
    }

    public void SetProjectileAttribute(ProjectileAttribute projectileAttribute) => _projectileAttribute = projectileAttribute; //Set Projectile Attribute when Instantiate
    public abstract void Trajectory(); //Detemine how projectile flying from source to destination and index of bullet

    public abstract float Damage(); //Detemine the scale of damage depend on source

    public void SetSourceAndDestination(UnitBase source, Vector3 position, Vector3 destination) { //Set Source and Destination when Instantiate
        this.source = source; 
        this.position = position;
        this.destination = destination;
    }

    private bool CheckOpponent(GameObject collider) { //Check if we hit opponent instead of wall...
        if(!source.CompareTag(collider.tag)) {
            Debug.Log(source.tag);
            if (!collider.CompareTag("Player") && !collider.CompareTag("Enemy")) return false;
            return true;
        }
        return false;
    }

    public virtual void OnTriggerEnter2D(Collider2D c) {
        //Debug.Log("Collided");
        
        if (c.gameObject.CompareTag(source.tag)) return;
        if(CheckOpponent(c.gameObject) && !collided) {
            if (!collided) collided = true;
            UnitBase cUnit = c.gameObject.GetComponent<UnitBase>();
            if(cUnit != null) {
                Debug.Log(source.stats.atk);

                cUnit.damagePosition = transform.position;

                source.Hitting(cUnit, Damage());
                
                Destroy(this.gameObject);
            }
        } else {
            Destroy(this.gameObject);
        }
    }

   public virtual void DestroyOnOverBounds() {
        if (Vector3.Distance(this.position, this.transform.position) > 20f) Destroy(gameObject);
    }

}
