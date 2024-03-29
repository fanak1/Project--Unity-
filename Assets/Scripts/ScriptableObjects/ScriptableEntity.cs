using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableEntity : ScriptableObject {

    public Faction faction;

    [SerializeField]
    public Stats stats { private set; get; } //For customize the stats of this Entity
    public Stats base_stats => stats; //base stats

    [SerializeField]
    public UnitBase prefabs { private set; get; } //Prefabs of this entity
}

[Serializable]
public class Stats {
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