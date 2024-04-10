using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScriptableRounds : ScriptableObject
{
    [SerializeField]
    public RoundInformation info { private set; get; } //Info of Stage

    [SerializeField]
    public List<EnemyCodeName> enemyList { private set; get; }

    public List<ScriptableEnemyUnit> LoadEnemyForRound() {
        List<ScriptableEnemyUnit> list = new List<ScriptableEnemyUnit>();
        foreach(EnemyCodeName enemy in enemyList) {
            var e = ResourceSystem.Instance.GetEnemyWithCodeName(enemy);
            list.Add(e);
        }
        return list;
    }

    //Cipher -- The question for this stage that can lead to difference occurence

    //Reward -- Reward after clear this stage
}

[Serializable]
public struct RoundInformation { //Information of stage
    public int roundValue; //The value that help random stage 
    public int round; //How many round of stage
    public int gold; //How many gold stage give
    public int numberOfEnemy; //How many enemy we have to clear to win this round
}

public enum RoundType {
    Normal = 0,
    Boss = 1
}

