using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[CreateAssetMenu(fileName = "NewRound", menuName = "Round")]
public class ScriptableRounds : ScriptableObject
{

    [SerializeField]
    public List<EnemyCodeName> enemyList;

    public List<ScriptableEnemyUnit> LoadEnemyForRound() {
        List<ScriptableEnemyUnit> list = new List<ScriptableEnemyUnit>();
        foreach(EnemyCodeName enemy in enemyList) {
            var e = Registry.Enemy(enemy);
            list.Add(e);
        }
        return list;
    }

    public void GetNewEnemyList(EnemyCodeName enemy, int count = 1) {
        List<EnemyCodeName> enemies = Enumerable.Repeat(enemy, count).ToList();

        this.enemyList = enemies;
    }

    //Cipher -- The question for this stage that can lead to difference occurence

    //Reward -- Reward after clear this stage
}

public enum RoundType {
    Normal = 0,
    Boss = 1
}

