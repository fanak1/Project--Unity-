using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableEntity : ScriptableObject {

    public Faction faction;

    [SerializeField]
    private Stats stats; //For customize the stats of this Entity
    public Stats base_stats => stats; //base stats

    [SerializeField]
    private List<ScriptableProjectiles> projectiles; //Storage of projectiles

    [SerializeField]
    private List<ScriptableAlbilities> abilities; //Storage of abilities

    [SerializeField]
    public UnitBase prefabs; //Prefabs of this entity

    public void SetStatsForEntity() => prefabs.stats = base_stats;

    public void SetStatsForEntity(Stats stats) {
        this.stats = stats;
        prefabs.stats = stats;
    }

    public void SetProjectileForEntity(UnitBase obj) {
        obj.InitProjecitle(projectiles);
    }

    public void SetAbilitiesForEntity(UnitBase obj) {
        obj.InitAbility(abilities);
    }

    public void BaseStatsChange(Stats stats) => this.stats = stats; //Call when stats change to modify stats

    public void AddProjectile(ScriptableProjectiles projectile) { //Call when Add new Projectile to modify projectile storage of this entity
        projectiles.Add(projectile);
    }

    public void AddAbility(ScriptableAlbilities albility) { //Call when Add new Abilities to modify abilitity list of this entity
        abilities.Add(albility);
    }

    internal void InitUnit() {
        SetStatsForEntity();
        prefabs.faction = faction;
    }

    public virtual UnitBase Spawn(Vector3 position) {
        InitUnit();
        var obj = Instantiate(prefabs, position, Quaternion.identity);
        obj.OnProjectileAdded += AddProjectile;
        obj.OnAbilityAdded += AddAbility;
        if (projectiles.Count > 0) obj.OnFinishInit += SetProjectileForEntity;
        if (abilities.Count > 0) obj.OnFinishInit += SetAbilitiesForEntity;
        return obj;
    }
}

[Serializable]
public struct Stats {
    public int hp;
    public int mp;
    public int atk;
    public int spd;
    public int def;
}


[Serializable]
public enum Faction {
    Player = 0,
    Ally = 1,
    Enemy = 2
}