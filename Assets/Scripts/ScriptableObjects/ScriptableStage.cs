using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStage", menuName = "Stages")]
public class ScriptableStage : ScriptableObject
{
    [SerializeField] private List<StageState> stateList; //List of event in this stage

    [SerializeField] private List<ScriptableRounds> roundList; //list of round in this stage

    public List<StageState> GetStateList() => stateList;

    public List<ScriptableRounds> GetRoundList() => roundList;

    public void ChangeEnemyForRound(EnemyCodeName enemy, int count, int index = 0) {
        if(roundList.Count > 0) {
            roundList[index].GetNewEnemyList(enemy, count);
        }
    }
}

[Serializable]
public enum StageState {
    Unready = -2,
    Ready = -1,
    Round = 0,
    Reward = 1,
    Finish = 2
}
