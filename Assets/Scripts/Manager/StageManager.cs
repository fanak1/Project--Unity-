using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StageManager : MonoBehaviour {
    public StageState state;

    [SerializeField] private List<ScriptableRounds> roundList; //list of round in this stage

    [SerializeField] private int roundIndex; //Index of round

    [SerializeField] private SpawnManager[] spawnList; //GameObject we create in Scene

    private int numberEnemyLeft; //number enemy we have to clear each round

    private bool stateInit = false; //For prevent init loop

    private void FinishAndSwitchState(StageState state) { //Swtich stage
        stateInit = false;
        this.state = state;
    }

    public void DecreaseEnemyOnDead() { //Decrease count for each enemy die
        this.numberEnemyLeft--;
    }

    private void SpawnEnemy(ScriptableEnemyUnit enemy, SpawnManager spawn) { //Spawn enemy at the spawn
        enemy.prefabs.OnDead += DecreaseEnemyOnDead;
        spawn.Spawn(enemy);
    }

    private void RoundState() {

        if (!stateInit) {

            List<ScriptableEnemyUnit> enemyList = new List<ScriptableEnemyUnit>(); //List of enemy in this round

            stateInit = true; //Prevent loop init

            enemyList = roundList[roundIndex].LoadEnemyForRound(); //Load enemy of this round

            numberEnemyLeft = roundList[roundIndex].info.numberOfEnemy; //number enemy we have to clear to win

            int firstSpawn = UnityEngine.Random.Range(0, numberEnemyLeft + 1); //number of enemy we spawn in first spawnpoint
            int secondSpawn = numberEnemyLeft - firstSpawn; //number of enemy we spawn in second spawnpoint

            int spawnFirst = UnityEngine.Random.Range(0, spawnList.Length+1); //where the first spawnpoint 
            int spawnSecond = 0;
            do {
                spawnSecond = UnityEngine.Random.Range(0, spawnList.Length+1);
            } while (spawnSecond != spawnFirst); //Find the second spawnpoint that distinguish with the first one

            for (int i = 0; i < firstSpawn; i++) { //Spawn enemy in first spawnpoint
                int enemyIndex = UnityEngine.Random.Range(0, enemyList.Count);
                SpawnEnemy(enemyList[enemyIndex], spawnList[spawnFirst]);
            }

            for(int i=0; i< secondSpawn; i++) { //Spawn enemy in second spawn point
                int enemyIndex = UnityEngine.Random.Range(0, enemyList.Count);
                SpawnEnemy(enemyList[enemyIndex], spawnList[spawnSecond]);
            }
            
        }
        if(numberEnemyLeft < 0) { //When we cleared this round
            roundIndex++;
            FinishAndSwitchState(StageState.Cipher);
        }
    }
}

[Serializable]
public enum StageState {
    Round = 0,
    Cipher = 1,
    Reward = 2,
    Finish = 3
}
