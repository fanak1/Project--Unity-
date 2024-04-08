using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class UnitBase : MonoBehaviour {

    public ProjectileHolder projectileHolder; //For projectile shoot

    [SerializeField] private DamagePopUps normalDamagePopUps; //DamagePopUp
    [SerializeField] private DamagePopUps critDamagePopUps; //Crit
                                                                                   
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

    public DamageScaleBonus damageScaleBonus = new DamageScaleBonus(0, 5f, 50f);


    //--------------------------------------------------------------------------------------------------------------------------------------------
    //Events

    //Function to called when the projectile succesfully collide with an object
    public virtual void Hitting(UnitBase target, float dmg) {

        OnHitting?.Invoke(target, dmg); // Invoke the functions when successfully hitting a target

        DealDamage(target, dmg);
    }

    
    /* Mainternance
    public virtual void Hit(UnitBase source, float dmg) { //source of dmg, how many dmg, and location where to display dmg popup
        OnHit?.Invoke(source, dmg);
    }
    */

    public event Action<UnitBase, float> OnDealDamage; //Use when succesfully deal damage to an object

    public event Action<UnitBase, float> OnHitting; // Use when successfully hitting an object

    public event Action<UnitBase, float> OnTakeDamage; // Use when take damage

    //public event Action<UnitBase, float> OnHit; //Use when being hit -----Mainternance

    
    // Create the UnityEvent when the object is awake
    private void SetUpEvent() {

        //OnHit += TakeDamage; //Add TakeDamage to OnTakeDamage event
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------



    // Battle event like TakeDamage, Heal, ReduceDef, Increase atk...

    internal virtual void TakeDamage(UnitBase source, float dmg, bool crit) { //Function to take damage
        float damageTaken = ReduceDamage(dmg);

        ReduceHealth(damageTaken);
        DamagePopUps(damageTaken, crit);

        OnTakeDamage?.Invoke(source, damageTaken);

        if (stats.hp <= 0) {
            //Dead event
        }
    }

    internal virtual void ReduceHealth(float dmgTaken) {
        stats.hp -= (int)dmgTaken;
    }

    protected virtual float ReduceDamage(float dmg) {
        float damageTaken = dmg / (1 + (stats.def / 200f));
        return damageTaken;
    }

    internal virtual void DealDamage(UnitBase target, float damage) {
        float randomCrit = UnityEngine.Random.Range(0f, 1f);
        bool crit;
        if(randomCrit < damageScaleBonus.critRate/100) {
            crit = true;
        } else {
            crit = false;
        }
        float damageDeal = DamageScale(damage, crit);
        target.TakeDamage(this, damageDeal, crit);

        OnDealDamage?.Invoke(target, damageDeal);
    }

    protected virtual float DamageScale(float dmg, bool crit) {
        if (crit) {
            return dmg * (1 + damageScaleBonus.damageBonus/100) * (1 + damageScaleBonus.critDamage/100);
        } else {
            return dmg * (1 + damageScaleBonus.damageBonus/100);
        }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------------



    //Ultility things
    private void DamagePopUps(float dmg, bool critted) { // Damage Pop Up when take damage
        float radX = UnityEngine.Random.Range(-0.1f, 0.1f);
        float radY = UnityEngine.Random.Range(-0.1f, 0.1f);
        if (!critted) {
            normalDamagePopUps.dmg = (int)dmg;
            Instantiate(normalDamagePopUps, damagePosition + Vector3.right * radX + Vector3.up * radY, Quaternion.identity);
        } else {
            critDamagePopUps.dmg = (int)dmg;
            Instantiate(critDamagePopUps, damagePosition + Vector3.right * radX + Vector3.up * radY, Quaternion.identity);
        }
        
    }
}

[Serializable]
public struct DamageScaleBonus {
    public float damageBonus;
    public float critRate;
    public float critDamage;

    public DamageScaleBonus(float damageBonus,  float critRate, float critDamage) {
        this.damageBonus = damageBonus;
        this.critRate = critRate;
        this.critDamage = critDamage;
    }
}
