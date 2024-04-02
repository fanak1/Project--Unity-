using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableEnemyUnit : ScriptableEntity
{
    [SerializeField]
    public EnemyType enemyType { private set; get; }

    [SerializeField]
    public EnemyCodeName enemyCodeName { private set; get;}
}

public enum EnemyType {
    Mobs = 0,
    Boss = 1
}

public enum EnemyCodeName {
    Bat = 0,

}


