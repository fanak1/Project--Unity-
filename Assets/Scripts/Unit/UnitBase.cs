using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MoreMountains.Tools;

public abstract class UnitBase : MonoBehaviour {

    public ProjectileHolder projectileHolder; //For projectile shoot
    public AlbilitiesHolder abilityHolder;

    [SerializeField] private DamagePopUps normalDamagePopUps; //DamagePopUp
    [SerializeField] private DamagePopUps critDamagePopUps; //Crit
    [SerializeField] private MMHealthBar mmHealthBar; //healthbar
                                                                                   
    public Vector3 damagePosition; //Positiion where Damage Pop Up

    //private Transform hpBar;//HP

    

    public Faction faction;

    public Stats stats;

    public DamageScaleBonus damageScaleBonus = new DamageScaleBonus(0, 5f, 50f);


    private float maxHP;
    private float nowHP;


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

    public event Action<UnitBase> OnFinishInit; //Use when finish init projectile holder, abilities holder ... for unitbase

    public event Action<UnitBase, float> OnDealDamage; //Use when succesfully deal damage to an object

    public event Action<UnitBase, float> OnHitting; // Use when successfully hitting an object

    public event Action<UnitBase, float> OnTakeDamage; // Use when take damage

    public event Action OnDead;

    public event Action<Stats> OnBaseStatsIncrease;

    public event Action<ScriptableProjectiles> OnProjectileAdded;

    public event Action<ScriptableAlbilities> OnAbilityAdded; 

    //public event Action<UnitBase, float> OnHit; //Use when being hit -----Mainternance

    
    // Create the UnityEvent when the object is awake
    private void SetUpEvent() {

        //OnHit += TakeDamage; //Add TakeDamage to OnTakeDamage event

        //LifeStreal

        //OnDealDamage += LifeSteal;

        OnDead += Destroy;
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------


    //Monobehaviour field

    protected virtual void Awake() {
        SetUpEvent(); //Init all event on awake
    }

    protected virtual void Start() {
        damagePosition = transform.position;
        projectileHolder = GetComponent<ProjectileHolder>();
        abilityHolder = GetComponent<AlbilitiesHolder>();
        mmHealthBar = GetComponent<MMHealthBar>();

        Initialize();

        OnFinishInit?.Invoke(this);
    }

    internal virtual void Update() {
        if (nowHP <= 0) {
            //Dead event
            OnDead?.Invoke();
            //Destroy(gameObject);
        }
    }



    //Stats ------------------------------------------------------------------------------------------------------------------------------------

    public Stats IncreaseStats(Stats stats) {
        this.stats.hp += stats.hp;
        this.stats.def += stats.def;
        this.stats.mp += stats.mp;
        this.stats.atk += stats.atk;
        this.stats.spd += stats.spd;
        return this.stats;
    }

    public void IncreaseBaseStats(Stats stats) {
        Stats newStats = IncreaseStats(stats);
        OnBaseStatsIncrease?.Invoke(newStats);
    }

    public void AddProjectile(ScriptableProjectiles projectile) {
        projectileHolder.AddProjectile(projectile);
        OnProjectileAdded?.Invoke(projectile);
    }

    public void AddAbility(ScriptableAlbilities ability) {
        abilityHolder.AddAbility(ability);
        OnAbilityAdded?.Invoke(ability);
    }

    //-------------------------------------------------------------------------------------------------------------------------------------------



    // Battle event like TakeDamage, Heal, ReduceDef, Increase atk...

    internal virtual void TakeDamage(UnitBase source, float dmg, bool crit) { //Function to take damage
        float damageTaken = ReduceDamage(dmg);

        ReduceHealth(damageTaken);
        DamagePopUps(damageTaken, crit);

        OnTakeDamage?.Invoke(source, damageTaken);

    }

    internal virtual void ReduceHealth(float dmgTaken) {
        //stats.hp -= (int)dmgTaken;
        nowHP -= dmgTaken;
        mmHealthBar.UpdateBar(nowHP, 0, maxHP, true);
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

    protected virtual void LifeSteal(UnitBase target, float dmg) {
        float damageDeal = target.ReduceDamage(dmg);
        stats.hp += (int)Heal(damageDeal);
    }

    protected virtual float Heal(float value) {
        return value;
    }

    protected virtual void IncreaseHP(int hp) {
        stats.hp += hp;
        maxHP = hp;
        mmHealthBar.UpdateBar(nowHP, 0f, maxHP, true); //Change HP Bar
    }

    internal virtual void Destroy() {
        Destroy(gameObject);
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


    internal virtual void Initialize() {
        maxHP = stats.hp;
        nowHP = maxHP;
        mmHealthBar.UpdateBar(nowHP, 0f, maxHP, true); //Change HP Bar
    }

    public virtual void InitProjecitle(List<ScriptableProjectiles> scriptableProjectiles) {
        foreach(ScriptableProjectiles p in scriptableProjectiles){
            projectileHolder.AddProjectile(p);
        }
    }

    public virtual void InitAbility(List<ScriptableAlbilities> scriptableAlbilities) {
        foreach(ScriptableAlbilities a in scriptableAlbilities) {
            abilityHolder.AddAbility(a);
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
