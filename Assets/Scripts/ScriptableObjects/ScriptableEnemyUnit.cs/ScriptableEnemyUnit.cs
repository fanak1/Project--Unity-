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

    public override void Spawn(Vector3 position) {
        OnSpawnEnemy?.Invoke();
        base.Spawn(position);
    }

}

[Serializable]
public enum EnemyType {
    Mobs = 0,
    Boss = 1
}

[Serializable]
public enum EnemyCodeName {
    Bat = 0,

}


