using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class UnitBase : MonoBehaviour {

    public ProjectileHolder projectileHolder;
    //DamagePopUp
    //HP

    private void Awake() {
        SetUpEvent(); //Init all event on awake
    }

    private void Start() {
        projectileHolder = GetComponent<ProjectileHolder>();
    }

    public Faction faction;

    public Stats stats;


    //Function to called when the projectile succesfully collide with an object
    public virtual void Hitting(UnitBase target, float dmg) {
        OnDealDamage?.Invoke(target, dmg); // Invoke the functions when successfully deal damage to a target
        OnHitting?.Invoke(target); // Invoke the functions when successfully hitting a target
    }

    //Function to called when the projectile succesfully collide with this object
    public virtual void Hit(UnitBase source, float dmg) {
        OnTakeDamage?.Invoke(dmg);
        OnHit?.Invoke(source);
    }


    public virtual void TakeDamage(float dmg) {
        stats.hp -= (int)dmg;
        if(stats.hp <= 0) {
            //Dead event
        }
    }

    public event Action<UnitBase, float> OnDealDamage; //Use when succesfully deal damage to an object

    public event Action<UnitBase> OnHitting; // Use when successfully hitting an object

    public event Action<float> OnTakeDamage; // Use when take damage

    public event Action<UnitBase> OnHit; //Use when being hit

    
    // Create the UnityEvent when the object is awake
    private void SetUpEvent() {
        OnTakeDamage += TakeDamage; //Add TakeDamage to OnTakeDamage event
    }
}