using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

public class StageManager : PersistentSingleton<StageManager> {

    public StageState state;

    [SerializeField] private ScriptableStage stage;

    [SerializeField] private int roundIndex; //Index of round

    [SerializeField] private int stateIndex; //Index of state

    [SerializeField] private SpawnPoint[] spawnList; //GameObject we create in Scene

    [SerializeField] private int numberEnemyLeft; //number enemy we have to clear each round

    private List<StageState> stateList;

    private List<ScriptableRounds> roundList;



    private bool stateInit = false; //For prevent init loop



    //Event ------------------------------------------------------------------------------------------------------------------------------------

    public event Action<int> OnRoundFinish; //On round "number" finish. ex: OnRoundFinish(1) is called when round 1 is finish

    public event Action OnCipherBegin;

    public event Action OnCipherFinish; //On answer the question

    public event Action OnRewardBegin;

    public event Action OnRewardFinish; //On reward of round "number" finish

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

    private IEnumerator SpawnEnemy(List<ScriptableEnemyUnit> enemy, SpawnPoint spawn) { //Spawn enemy at the spawn

        StartCoroutine(spawn.BeginSpawn());

        yield return new WaitForSeconds(1f);
        
        var obj = spawn.SpawnWithDelay(enemy);
        foreach(var e in obj) { 
            e.OnDead += DecreaseEnemyOnDead;
        }
    }


    private void ReadyState() {
        if (!stateInit) {
            stateInit = true; //Call when done animation of Stage Informing
        } else {
            stateInit = false;
            this.state = stateList[stateIndex];
        }
    }

    private void RoundState() { //Round state - 0

        if (!stateInit) {

            List<ScriptableEnemyUnit> enemyList = new List<ScriptableEnemyUnit>(); //List of enemy in this round

            stateInit = true; //Prevent loop init

            enemyList = roundList[roundIndex].LoadEnemyForRound(); //Load enemy of this round

            numberEnemyLeft = roundList[roundIndex].info.numberOfEnemy; //number enemy we have to clear to win

            int firstSpawn = UnityEngine.Random.Range(0, (numberEnemyLeft + 1) / 2); //number of enemy we spawn in first spawnpoint
            int secondSpawn = numberEnemyLeft - firstSpawn; //number of enemy we spawn in second spawnpoint

            int spawnFirst = UnityEngine.Random.Range(0, spawnList.Length); //where the first spawnpoint 
            int spawnSecond;
            do {
                spawnSecond = UnityEngine.Random.Range(0, spawnList.Length);
            } while (spawnSecond == spawnFirst); //Find the second spawnpoint that distinguish with the first one

            List<ScriptableEnemyUnit> enemyList1 = new List<ScriptableEnemyUnit>();
            List<ScriptableEnemyUnit> enemyList2 = new List<ScriptableEnemyUnit>();

            for (int i = 0; i < firstSpawn; i++) { //Spawn enemy in first spawnpoint
                int enemyIndex = UnityEngine.Random.Range(0, enemyList.Count);
                enemyList1.Add(enemyList[enemyIndex]);
                
            }

            for(int i=0; i< secondSpawn; i++) { //Spawn enemy in second spawn point
                int enemyIndex = UnityEngine.Random.Range(0, enemyList.Count);
                enemyList2.Add(enemyList[enemyIndex]);

            }
            StartCoroutine(SpawnEnemy(enemyList1, spawnList[spawnFirst]));
            StartCoroutine(SpawnEnemy(enemyList2, spawnList[spawnSecond]));
            
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
        if (!stateInit) {
            OnRewardBegin?.Invoke();
            stateInit = true;
        }
    }

    public void RewardStateEnd() {
        OnRewardFinish?.Invoke();
        FinishAndSwitchState();
    }

    private void FinishState() {
        if (!stateInit) {
            Debug.Log("done");
            stateInit = true;
            OnStageFinish?.Invoke();
        }
        
    }

    public void ChangeStage(ScriptableStage stage) {
        this.stage = stage;
        this.Start();
    }

    public void Ready() {
        state = StageState.Ready;
    }


    internal virtual void Start() {
        roundIndex = 0;
        stateIndex = 0;
        stateInit = false;

        stateList = stage.GetStateList();

        roundList = stage.GetRoundList();

        state = StageState.Unready;
    }

    internal virtual void Update() {
        switch (state) {
            case StageState.Ready:
                ReadyState();
                break;
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

