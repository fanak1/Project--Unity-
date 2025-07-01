using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Enemy")]
public class ScriptableEnemyUnit : ScriptableEntity
{

    [SerializeField]
    public EnemyType enemyType;

    [SerializeField]
    public EnemyCodeName enemyCodeName;

    public event Action OnSpawnEnemy;

    public override UnitBase Spawn(Vector3 position, List<ScriptableAlbilities> abilities = null, List<ScriptableProjectiles> projectiles = null) {
        OnSpawnEnemy?.Invoke();
        var obj = base.Spawn(position);
        return obj;
    }

}

[Serializable]
public enum EnemyType {
    Mobs = 0,
    Boss = 1
}

[Serializable]
public enum EnemyCodeName {
    Bat,
    Orc,
    Stone_Boss,
    GreenBat
}


