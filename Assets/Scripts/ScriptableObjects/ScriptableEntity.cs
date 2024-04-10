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
    public UnitBase prefabs; //Prefabs of this entity

    public void SetStatsForEntity() => prefabs.stats = base_stats;

    public void SetStatsForEntity(Stats stats) => prefabs.stats = stats;

    private void InitUnit() {
        SetStatsForEntity();
        prefabs.faction = faction;
    }

    public virtual void Spawn(Vector3 position) {
        InitUnit();
        Instantiate(prefabs, position, Quaternion.identity);
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