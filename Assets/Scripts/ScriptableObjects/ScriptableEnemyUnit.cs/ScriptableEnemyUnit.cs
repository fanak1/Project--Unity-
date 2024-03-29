using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableEnemyUnit : ScriptableEntity
{
    public Faction faction = Faction.Enemy;
}

public enum EnemyType {
    Mobs = 0,
    Boss = 1
}
