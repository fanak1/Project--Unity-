using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class UnitBase : MonoBehaviour {

    public ProjectileHolder projectileHolder; //For projectile shoot
    public AlbilitiesHolder abilityHolder;

    public Sprite icon;

    //[SerializeField] private MMHealthBar mmHealthBar; //healthbar

    public Vector3 damagePosition; //Positiion where Damage Pop Up

    //private Transform hpBar;//HP



    public Faction faction;

    public Stats stats;

    public Stats base_stats => stats;

    public CharacterCode characterCode;

    public DamageScaleBonus damageScaleBonus = new DamageScaleBonus(0, 5f, 50f);


    internal float maxHP;
    internal float nowHP;

    internal float maxMP;
    internal float nowMP;

    internal float regenMP = 50f;
    internal float regenHP = 20f;

    public int level = 1;
    public int deltaLevel = 0;


    //--------------------------------------------------------------------------------------------------------------------------------------------
    //Events

    //Function to called when the projectile succesfully collide with an object
    public virtual void Hitting(UnitBase target, float dmg) {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.hitEnemy);
        OnHitting?.Invoke(target, dmg); // Invoke the functions when successfully hitting a target

        DealDamage(target, dmg);
    }


    /* Mainternance
    public virtual void Hit(UnitBase source, float dmg) { //source of dmg, how many dmg, and location where to display dmg popup
        OnHit?.Invoke(source, dmg);
    }
    */

    public event Action<UnitBase> OnFinishInit; //Use when finish init projectile holder, abilities holder ... for unitbase

    public event Action<UnitBase> OnFinishInitProjectile; //Use when finish init projectile holder for unitbase, so that we can init abilities after init projectile holder

    public event Action<Projectiles, Vector3, Vector3> OnProjectileShoot; //Use when shoot a projectile, so that we can do something at the position of this event

    public event Action<UnitBase, float> OnDealDamage; //Use when succesfully deal damage to an object

    public event Action<UnitBase, float> OnHitting; // Use when successfully hitting an object

    public event Action<UnitBase, float> OnTakeDamage; // Use when take damage

    public event Action OnDead;

    public event Action<UnitBase> OnKill; // Use when kill an object

    public event Action<Stats> OnBaseStatsIncrease;

    public event Action<ScriptableProjectiles> OnProjectileAdded;

    public event Action<ScriptableAlbilities> OnAbilityAdded;

    public bool dead = false;

    public bool onKilledTrigger = false; //Use when this unitbase is killed, so that we can trigger some event like spawn item, gold, exp... when this unitbase is killed

    //public event Action OnAbilityKeyPressed;

    //public event Action<UnitBase, float> OnHit; //Use when being hit -----Mainternance


    // Create the UnityEvent when the object is awake
    private void SetUpEvent() {

        //OnHit += TakeDamage; //Add TakeDamage to OnTakeDamage event

        //LifeStreal

        //OnDealDamage += LifeSteal;

        //OnDead += Destroy;
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
        

        InitializeHP();
        InitializeMP();

        OnFinishInit?.Invoke(this);
    }

    public void CanInitAbility()
    {
        OnFinishInitProjectile?.Invoke(this); //Invoke when finish init projectile holder, so that we can init abilities after init projectile holder
    }


    internal virtual void UpdateFunction()
    {
        
        if (nowHP <= 0 && !dead)
        {
            //Dead event
            OnDead?.Invoke();
            Destroy();
            dead = true;
            //Destroy(gameObject);
        }
        //OnAbilityKeyPressed?.Invoke();

        RegenMP(regenMP);
    }

    private void Update() {
        if (GameManager.Instance.pause)
            return;
        UpdateFunction();
    }



    //Stats ------------------------------------------------------------------------------------------------------------------------------------

    public void SetLevel(int level, bool exceptSpd = true) {
        int deltaLevel = level - this.level;
        this.level = level;
        int levelScale = this.deltaLevel * deltaLevel;
        Stats incStats = new Stats(levelScale);
        if (exceptSpd) incStats.spd = 0;
        IncreaseStats(incStats);
    }

    public Stats IncreaseStats(Stats stats) {

        //this.stats.hp += stats.hp;
        IncreaseMaxHP(stats.hp);

        //this.stats.mp += stats.mp;
        IncreaseMaxMP(stats.mp);

        this.stats.def += stats.def;
        
        this.stats.atk += stats.atk;
        this.stats.spd += stats.spd;

        this.maxHP = this.stats.hp;
        this.maxMP = this.stats.mp;

        return this.stats;
    }

    public Stats DecreaseStats(Stats stats) {

        //this.stats.hp -= stats.hp;
        IncreaseMaxHP(-stats.hp);

        //this.stats.mp -= stats.mp;
        IncreaseMaxMP(-stats.mp);

        this.stats.def -= stats.def;
        
        this.stats.atk -= stats.atk;
        this.stats.spd -= stats.spd;

        

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
        abilityHolder.ClaimAbility(ability);
        OnAbilityAdded?.Invoke(ability);
    }

    public void DeleteAbility(ScriptableAlbilities ability) {
        abilityHolder.DeleteAbility(ability);

    }

    //-------------------------------------------------------------------------------------------------------------------------------------------



    // Battle event like TakeDamage, Heal, ReduceDef, Increase atk...

    internal virtual void TriggerShootedProjectiles(Projectiles projectile, Vector3 position, Vector3 destination)
    {
        OnProjectileShoot?.Invoke(projectile, position, destination);
    }

    internal virtual void TakeDamage(UnitBase source, float dmg, bool crit) { //Function to take damage
        float damageTaken = ReduceDamage(dmg);

        ReduceHealth(damageTaken);
        DamagePopUps(damageTaken, crit);

        OnTakeDamage?.Invoke(source, damageTaken);

        if(nowHP <= 0 && !onKilledTrigger)
        {
            source.OnKill?.Invoke(this);
            onKilledTrigger = true;
        }

    }

    internal virtual void ReduceHealth(float dmgTaken) {
        //stats.hp -= (int)dmgTaken;
        nowHP -= dmgTaken;  
    }

    internal virtual void ReduceMP(float mpTaken) {
        nowMP -= mpTaken;
    }

    protected virtual float ReduceDamage(float dmg) {
        float damageTaken = dmg / (1 + (stats.def / 200f));
        return damageTaken;
    }

    internal virtual void DealDamage(UnitBase target, float damage) {
        float randomCrit = UnityEngine.Random.Range(0f, 1f);
        bool crit;
        if (randomCrit < damageScaleBonus.critRate / 100) {
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
            return dmg * (1 + damageScaleBonus.damageBonus / 100) * (1 + damageScaleBonus.critDamage / 100);
        } else {
            return dmg * (1 + damageScaleBonus.damageBonus / 100);
        }
    }

    protected virtual void LifeSteal(UnitBase target, float dmg) {
        float damageDeal = target.ReduceDamage(dmg);
        stats.hp += (int)Heal(damageDeal);
    }

    protected virtual float Heal(float value) {
        return value;
    }

    internal virtual void IncreaseMaxHP(int hp) {
        stats.hp += hp;
        maxHP = stats.hp;
        
    }

    internal virtual void IncreaseMaxMP(int mp) {
        stats.mp += mp;
        maxMP = stats.mp;
    }

    internal virtual void RegenMP(float regenSpeed) {
        if(nowMP < maxMP) {
            nowMP += regenSpeed * Time.deltaTime;
        }
    }

    internal virtual void Destroy() {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.killEnemy);
        Destroy(gameObject);
    }

    //------------------------------------------------------------------------------------------------------------------------------------------------



    //Ultility things
    private void DamagePopUps(float dmg, bool critted) {
        DamagePopUpSpawner.Instance.ShowDamage(damagePosition, (int)dmg, critted);
    }


    internal virtual void InitializeHP() {
        maxHP = stats.hp;
        nowHP = maxHP;
        
    }

    internal virtual void InitializeMP() {
        maxMP = stats.mp;
        nowMP = maxMP;
    }

    public virtual void InitProjecitle(List<ScriptableProjectiles> scriptableProjectiles) {
        foreach (ScriptableProjectiles p in scriptableProjectiles) {
            projectileHolder.AddProjectile(p);
        }
    }

    public virtual void InitAbility(List<ScriptableAlbilities> scriptableAlbilities) {
        foreach (ScriptableAlbilities a in scriptableAlbilities) {
            abilityHolder.AddAbility(a);
        }
    }

    public virtual Stats ShowStats() => this.stats;

    public virtual Stats ShowBaseStats() => this.base_stats;

    public virtual Stats ShowIncreaseStats() => abilityHolder.ShowIncreaseStats();

    public virtual List<ScriptableAlbilities> ShowAbilities() {
        if (abilityHolder != null) {
            return this.abilityHolder.list;
        } else {
            return null;
        }
    }


    public virtual List<Abilities> ShowActiveAbilities() {
        if (abilityHolder != null) {
            return this.abilityHolder.active;
        } else {
            return null;
        }
    }

    public virtual List<Abilities> ShowPassiveAbilities() {
        if (abilityHolder != null) {
            return this.abilityHolder.passive;
        } else {
            return null;
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
