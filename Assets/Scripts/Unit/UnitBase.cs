using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class UnitBase : MonoBehaviour {

    public ProjectileHolder projectileHolder; //For projectile shoot

    [SerializeField] private DamagePopUps damagePopUps; //DamagePopUp
    public Vector3 damagePosition; //Positiion where Damage Pop Up

    //private Transform hpBar;//HP

    protected virtual void Awake() {
        SetUpEvent(); //Init all event on awake
    }

    protected virtual void Start() {
        damagePosition = transform.position;
        projectileHolder = GetComponent<ProjectileHolder>();
    }

    public Faction faction;

    public Stats stats;


    //-----------------------------------------------------------------------------------------------------------------------------------
    //Events

    //Function to called when the projectile succesfully collide with an object
    public virtual void Hitting(UnitBase target, float dmg) {
        OnHitting?.Invoke(target, dmg); // Invoke the functions when successfully hitting a target
    }

    //Function to called when the projectile succesfully collide with this object
    public virtual void Hit(UnitBase source, float dmg) { //source of dmg, how many dmg, and location where to display dmg popup
        OnHit?.Invoke(source, dmg);
    }

    public event Action<UnitBase, float> OnDealDamage; //Use when succesfully deal damage to an object

    public event Action<UnitBase, float> OnHitting; // Use when successfully hitting an object

    public event Action<UnitBase, float> OnTakeDamage; // Use when take damage

    public event Action<UnitBase, float> OnHit; //Use when being hit

    
    // Create the UnityEvent when the object is awake
    private void SetUpEvent() {

        OnHit += TakeDamage; //Add TakeDamage to OnTakeDamage event
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------



    // Battle event like TakeDamage, Heal, ReduceDef, Increase atk...

    internal virtual void TakeDamage(UnitBase source, float dmg) { //Function to take damage
        float damageTaken = DamageTakenScale(dmg);
        Debug.Log(damageTaken);
        stats.hp -= (int)damageTaken;
        DamagePopUps(damageTaken);
        OnTakeDamage?.Invoke(source, damageTaken);
        source.OnDealDamage?.Invoke(this, damageTaken);
        if (stats.hp <= 0) {
            //Dead event
        }
    }

    protected virtual float DamageTakenScale(float dmg) {
        float damageTaken = dmg / (1 + (stats.def / 200f));
        return damageTaken;
    }

    //------------------------------------------------------------------------------------------------------------------------------------------------



    //Ultility things
    private void DamagePopUps(float dmg) { // Damage Pop Up when take damage
        float radX = UnityEngine.Random.Range(-0.1f, 0.1f);
        float radY = UnityEngine.Random.Range(-0.1f, 0.1f);
        damagePopUps.dmg = (int)dmg;
        Instantiate(damagePopUps, damagePosition + Vector3.right * radX + Vector3.up * radY, Quaternion.identity);
    }
}
