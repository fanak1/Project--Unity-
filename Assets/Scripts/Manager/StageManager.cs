using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StageManager : PersistentSingleton<StageManager> {
    public StageState state;

    [SerializeField] private List<StageState> stateList; //List of event in this stage;

    [SerializeField] private int stateIndex;

    [SerializeField] private List<ScriptableRounds> roundList; //list of round in this stage

    [SerializeField] private int roundIndex; //Index of round

    [SerializeField] private SpawnManager[] spawnList; //GameObject we create in Scene

    [SerializeField] private int numberEnemyLeft; //number enemy we have to clear each round

    private bool stateInit = false; //For prevent init loop



    //Event ------------------------------------------------------------------------------------------------------------------------------------

    public event Action<int> OnRoundFinish; //On round "number" finish. ex: OnRoundFinish(1) is called when round 1 is finish

    public event Action OnCipherBegin;

    public event Action OnCipherFinish; //On answer the question

    public event Action<int> OnRewardFinish; //On reward of round "number" finish

    public event Action OnStageFinish; //On Clear Stage


    //------------------------------------------------------------------------------------------------------------------------------------------

    private void FinishAndSwitchState() { //Swtich stage
        stateInit = false;
        stateIndex++;
        this.state = stateList[stateIndex];
    }

    public void DecreaseEnemyOnDead() { //Decrease count for each enemy die
        Debug.Log("die");
        numberEnemyLeft -= 1;
    }

    private void SpawnEnemy(ScriptableEnemyUnit enemy, SpawnManager spawn) { //Spawn enemy at the spawn
        var obj = spawn.Spawn(enemy);
        obj.OnDead += DecreaseEnemyOnDead;
    }

    private void RoundState() { //Round state - 0

        if (!stateInit) {

            List<ScriptableEnemyUnit> enemyList = new List<ScriptableEnemyUnit>(); //List of enemy in this round

            stateInit = true; //Prevent loop init

            enemyList = roundList[roundIndex].LoadEnemyForRound(); //Load enemy of this round

            numberEnemyLeft = roundList[roundIndex].info.numberOfEnemy; //number enemy we have to clear to win

            int firstSpawn = UnityEngine.Random.Range(0, numberEnemyLeft + 1); //number of enemy we spawn in first spawnpoint
            int secondSpawn = numberEnemyLeft - firstSpawn; //number of enemy we spawn in second spawnpoint

            int spawnFirst = UnityEngine.Random.Range(0, spawnList.Length); //where the first spawnpoint 
            int spawnSecond;
            do {
                spawnSecond = UnityEngine.Random.Range(0, spawnList.Length);
            } while (spawnSecond == spawnFirst); //Find the second spawnpoint that distinguish with the first one

            for (int i = 0; i < firstSpawn; i++) { //Spawn enemy in first spawnpoint
                int enemyIndex = UnityEngine.Random.Range(0, enemyList.Count);
                SpawnEnemy(enemyList[enemyIndex], spawnList[spawnFirst]);
                
            }

            for(int i=0; i< secondSpawn; i++) { //Spawn enemy in second spawn point
                int enemyIndex = UnityEngine.Random.Range(0, enemyList.Count);
                SpawnEnemy(enemyList[enemyIndex], spawnList[spawnSecond]);

            }
            
        }
        
        if(numberEnemyLeft <= 0) { //When we cleared this round

            OnRoundFinish?.Invoke(roundIndex);
            FinishAndSwitchState();
            roundIndex++;
        }
    }

    private void CipherState() {
        if (!stateInit) { 
            OnCipherBegin?.Invoke();
            stateInit = true;
        }
    }

    public void CipherStateEnd() {
        OnCipherFinish?.Invoke();
        FinishAndSwitchState();
    }

    private void RewardState() {

    }

    private void FinishState() {
        if (!stateInit) {
            Debug.Log("done");
            stateInit = true;
        }
        
    }


    internal virtual void Start() {
        roundIndex = 0;
        stateIndex = 0;

    }

    internal virtual void Update() {
        switch (state) {
            case StageState.Round:
                RoundState();
                break;
            case StageState.Cipher:
                CipherState();
                break;
            case StageState.Reward:
                RewardState();
                break;
            case StageState.Finish:
                FinishState();
                break;
            default:
                break;
        }
    }
}

[Serializable]
public enum StageState {
    Ready = -1,
    Round = 0,
    Cipher = 1,
    Reward = 2,
    Finish = 3
}
