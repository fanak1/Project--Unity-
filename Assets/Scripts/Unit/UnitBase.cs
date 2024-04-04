using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class UnitBase : MonoBehaviour {

    public ProjectileHolder projectileHolder;

    private void Awake() {
        SetUpEvent(); //Init all event on awake
    }

    private void Start() {
        projectileHolder = GetComponent<ProjectileHolder>();
    }

    public Faction faction;

    public Stats stats;


    //Function to called when the projectile succesfully collide with an object
    public virtual void Hitting(UnitBase target, int dmg) {
        OnDealDamage.Invoke(target, dmg); // Invoke the functions when successfully deal damage to a target
        OnHitting.Invoke(target); // Invoke the functions when successfully hitting a target
    }

    //Function to called when the projectile succesfully collide with this object
    public virtual void Hit(UnitBase source, int dmg) {
        OnTakeDamage.Invoke(dmg);
        OnHit.Invoke(source);
    }


    public virtual void TakeDamage(int dmg) {
        stats.hp -= dmg;
        if(stats.hp <= 0) {
            //Dead event
        }
    }

    public UnityEvent<UnitBase, int> OnDealDamage; //Use when succesfully deal damage to an object

    public UnityEvent<UnitBase> OnHitting; // Use when successfully hitting an object

    public UnityEvent<int> OnTakeDamage; // Use when take damage

    public UnityEvent<UnitBase> OnHit; //Use when being hit

    
    // Create the UnityEvent when the object is awake
    private void SetUpEvent() {
        if (OnDealDamage == null) {
            OnDealDamage = new UnityEvent<UnitBase, int>();
        }
        if (OnHitting == null) {
            OnHitting = new UnityEvent<UnitBase>();
        }
        if (OnTakeDamage == null) {
            OnTakeDamage = new UnityEvent<int>();
        }
        if (OnHit == null) {
            OnHit = new UnityEvent<UnitBase>();
        }

        OnTakeDamage.AddListener(TakeDamage); //Add TakeDamage to OnTakeDamage event
    }
}
